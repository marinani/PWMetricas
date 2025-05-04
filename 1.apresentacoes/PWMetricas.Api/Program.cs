using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using PWMetricas.Dados;
using PWMetricas.Configuracao;

var builder = WebApplication.CreateBuilder(args);





// Add services to the container.
ApiDependencias.Init(builder.Services, builder.Configuration);

// Configurar logging detalhado
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

app.Use(async (context, next) =>
{
    await next();
});


// Configure the HTTP request pipeline.
ApiDependencias.Configurar(app);
app.Run();
