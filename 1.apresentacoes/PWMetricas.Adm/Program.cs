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


#region Serviços
builder.Services.AddScoped<IPerfilServico, PerfilServico>();
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();
#endregion

#region Repositórios
builder.Services.AddScoped<IPerfilRepositorio, PerfilRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
#endregion

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
