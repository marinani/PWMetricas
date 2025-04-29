using Microsoft.EntityFrameworkCore;
using PWMetricas.Dados;
using PWMetricas.Aplicacao.Servicos;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios;
using PWMetricas.Dados.Repositorios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuração do DbContext
builder.Services.AddDbContext<PwMetricasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PwMetricasDbConnection"), sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    }));

// Registro do AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient<CepServico>();

#region Serviços
builder.Services.AddScoped<IPerfilServico, PerfilServico>();
builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();
builder.Services.AddScoped<IClienteServico, ClienteServico>();
builder.Services.AddScoped<ICanalServico, CanalServico>();
builder.Services.AddScoped<ITamanhoServico, TamanhoServico>();
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();
builder.Services.AddScoped<IOrigemServico, OrigemServico>();
builder.Services.AddScoped<ILojaServico, LojaServico>();
builder.Services.AddScoped<IStatusAtendimentoServico, StatusAtendimentoServico>();
builder.Services.AddScoped<IAtendimentoServico, AtendimentoServico>();
#endregion

#region Repositórios
builder.Services.AddScoped<IPerfilRepositorio, PerfilRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<ICanalRepositorio, CanalRepositorio>();
builder.Services.AddScoped<ITamanhoRepositorio, TamanhoRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<IOrigemRepositorio, OrigemRepositorio>();
builder.Services.AddScoped<ILojaRepositorio, LojaRepositorio>();
builder.Services.AddScoped<IStatusAtendimentoRepositorio, StatusAtendimentoRepositorio>();
builder.Services.AddScoped<IAtendimentoRepositorio, AtendimentoRepositorio>();
builder.Services.AddScoped<IAtendimentoObservacoesRepositorio, AtendimentoObservacoesRepositorio>();
#endregion

// Configuração de autenticação
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login/Index"; // Caminho para a tela de login
        options.AccessDeniedPath = "/Login/AcessoNegado"; // Caminho para acesso negado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tempo de expiração do cookie (30 minutos)
        options.SlidingExpiration = true; // Renova o tempo de expiração se o usuário estiver ativo

    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AcessoAdministrador", policy =>
        policy.RequireRole("Administrador"));
    options.AddPolicy("AcessoGerente", policy =>
        policy.RequireRole("Gerente"));
    options.AddPolicy("AcessoVendedor", policy =>
        policy.RequireRole("Vendedor"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        ctx.Context.Response.Headers["Pragma"] = "no-cache";
        ctx.Context.Response.Headers["Expires"] = "0";
    }
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
