using Microsoft.Extensions.DependencyInjection;
using PWMetricas.Dados.Repositorios;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Aplicacao.Servicos;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Adm.Injecoes
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AdmDependencias(this IServiceCollection services)
        {

            #region Serviços
            services.AddScoped<IProdutoServico, ProdutoServico>();
            #endregion

            #region Repositórios
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            #endregion



            return services;
        }
    }
}
