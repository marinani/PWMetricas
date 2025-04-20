using Microsoft.Extensions.DependencyInjection;
using PWMetricas.Dados.Repositorios;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Aplicacao.Servicos;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Adm.Injecoes
{
    public static class AdmDependencias
    {

        public static void Init(IServiceCollection servicos, IConfiguration configuracao, IHostEnvironment ambiente)
        {
            TemplateServicos(servicos, configuracao);
        }

        public static void Configurar(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseResponseCompression();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCaching();
            app.MapControllerRoute("default", "{controller=Home}/{action=Inicio}/{chave:guid?}");


            var supportedCultures = new[] { "pt-BR" };

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

        }


        private static void TemplateServicos(IServiceCollection servicos, IConfiguration configuracao)
        {
            #region Serviços
            servicos.AddScoped<IPerfilServico, PerfilServico>();
            servicos.AddScoped<IProdutoServico, ProdutoServico>();
            #endregion

            #region Repositórios
            servicos.AddScoped<IPerfilRepositorio, PerfilRepositorio>();
            servicos.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            #endregion
        }

      
    }
}
