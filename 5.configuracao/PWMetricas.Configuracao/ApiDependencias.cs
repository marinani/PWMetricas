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
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pw de Gerenciamento de Métricas - API - v1.0");
                c.RoutePrefix = string.Empty;

                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "API de Gerenciamento de Métricas - v1.0");
                c.RoutePrefix = "docs"; // Define o Swagger UI em "/docs"
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
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Pw Metricas API",
                        Description = "API REST do sistema de gestão de métricas.",
                        Contact = new OpenApiContact
                        {
                            Name = "Pw Midia",
                            Email = "contato@pwmidia.com"
                        }
                    });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = @"Token de acesso para autenticação na API, gerado através do método ""/api/autenticacao"". Exemplo (enviar no header das requisições): Bearer {token}.",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                 {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                     },
                     Array.Empty<string>()
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
            var secretKey = configuration.GetSection("Jwt")["Key"] ?? string.Empty;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = tokenValidationParameters);

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, _ =>
                {
                    _.Cookie.HttpOnly = true;
                });


            services.AddAuthorization(options =>
            {
                foreach (var pcv in Policy.Compilar(configuration))
                {
                    options.AddPolicy(pcv.Policy, policy => policy.RequireClaim(pcv.ClaimType, pcv.ClaimValue));
                }
            });

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });
        }
    }
}
