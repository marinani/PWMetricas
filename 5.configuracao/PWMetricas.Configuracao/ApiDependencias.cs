using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PWMetricas.Aplicacao.Servicos;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Aplicacao.Autenticacao.API;
using PWMetricas.Dados;
using PWMetricas.Configuracao.Politicas;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dados.Repositorios;

namespace PWMetricas.Configuracao
{
    public static class ApiDependencias
    {
        public static void Configurar(WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pw Métricas - API - v1.0");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }

        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            Clientes(services);
            Contextos(services, configuration);
            TemplateServicos(services, configuration);

            services.AddControllers(config =>
            {
                // Usa a política padrão definida no AddAuthorization (com o claim Operacoes.Listar)
                config.Filters.Add(new AuthorizeFilter());
            });

            services.AddEndpointsApiExplorer();
            services.AddCors();

            MontarAutenticacao(services, configuration);

            services.AddSingleton<Token>();
            services.AddSingleton<IJsonServico, JsonServico>();

            var tokens = configuration.GetSection("TokenSistemas").GetChildren();
            services.Configure<Tokens>(options =>
            {
                foreach (var item in tokens)
                {
                    options.Sistemas.Add(item.Key, Guid.Parse(item.Value));
                }
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pw Métricas API",
                    Description = "API para gerenciamento de métricas."
                });

                // Configuração para autenticação JWT no Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Insira o token JWT no formato: Bearer {seu token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
            });

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }

        private static void TemplateServicos(IServiceCollection servicos, IConfiguration configuracao)
        {
            servicos.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            servicos.AddHttpClient<CepServico>();

            #region Serviços
            servicos.AddScoped<IPerfilServico, PerfilServico>();
            servicos.AddScoped<IUsuarioServico, UsuarioServico>();
            servicos.AddScoped<IClienteServico, ClienteServico>();
            servicos.AddScoped<ICanalServico, CanalServico>();
            servicos.AddScoped<ITamanhoServico, TamanhoServico>();
            servicos.AddScoped<IProdutoServico, ProdutoServico>();
            servicos.AddScoped<IOrigemServico, OrigemServico>();
            servicos.AddScoped<ILojaServico, LojaServico>();
            servicos.AddScoped<IStatusAtendimentoServico, StatusAtendimentoServico>();
            servicos.AddScoped<IAtendimentoServico, AtendimentoServico>();
            #endregion

            #region Repositórios
            servicos.AddScoped<IPerfilRepositorio, PerfilRepositorio>();
            servicos.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            servicos.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            servicos.AddScoped<ICanalRepositorio, CanalRepositorio>();
            servicos.AddScoped<ITamanhoRepositorio, TamanhoRepositorio>();
            servicos.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            servicos.AddScoped<IOrigemRepositorio, OrigemRepositorio>();
            servicos.AddScoped<ILojaRepositorio, LojaRepositorio>();
            servicos.AddScoped<IStatusAtendimentoRepositorio, StatusAtendimentoRepositorio>();
            servicos.AddScoped<IAtendimentoRepositorio, AtendimentoRepositorio>();
            servicos.AddScoped<IAtendimentoObservacoesRepositorio, AtendimentoObservacoesRepositorio>();
            #endregion
        }

        private static void Clientes(IServiceCollection servicos) => servicos.AddHttpClient();

        private static void Contextos(IServiceCollection servicos, IConfiguration configuracao)
        {
            servicos
                 .AddDbContext<PwMetricasDbContext>(_ => _
                     .UseSqlServer(configuracao.GetConnectionString("PwMetricasDbConnection")));

            var connectionStrings = configuracao.GetSection("ConnectionStrings").GetChildren().ToList();
            Dominio.Configuracao.ConexaoPrincipal = Convert.ToString(connectionStrings[0].Value);
        }

        private static void MontarAutenticacao(IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["Jwt:Key"];
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = configuration["Jwt:Issuer"],
             ValidAudience = configuration["Jwt:Audience"],
             IssuerSigningKey = signingKey,
             ClockSkew = TimeSpan.Zero // Remove tolerância de tempo
         };

         // Adicionar eventos para depuração
         options.Events = new JwtBearerEvents
         {
             OnAuthenticationFailed = context =>
             {
                 Console.WriteLine($"Token inválido: {context.Exception.Message}");
                 return Task.CompletedTask;
             },
             OnTokenValidated = context =>
             {
                 Console.WriteLine("Token validado com sucesso.");
                 return Task.CompletedTask;
             }
         };
     });

            services.AddAuthorization(options =>
            {
                var defaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                options.DefaultPolicy = defaultPolicy;
            });

         
        }
    }
}
