using Microsoft.Extensions.Configuration;

namespace PWMetricas.Aplicacao.Autenticacao.API
{
    /// <summary>
    /// Extensões para Microsoft.Extensions.Configuration.IConfiguration.
    /// </summary>
    public static class IConfigurationExtensions
    {
        /// <summary>
        /// Converter uma configuração em um dicionário comum.
        /// </summary>
        /// <param name="configuration">Configuração a ser convertida.</param>
        /// <returns></returns>
        public static Dictionary<string, object> AsDictionary(this IConfiguration configuration)
        {
            var dic = new Dictionary<string, object>();
            foreach (var el in configuration.GetChildren())
            {
                if (el.Value != null)
                    dic[el.Key] = el.Value;
                else
                    dic[el.Key] = AsDictionary(el.GetChildren());
            }
            return dic;
        }

        /// <summary>
        /// Converter uma agrupamento de sessão de configuração em um dicionário comum.
        /// </summary>
        /// <param name="sectionItems">Agrupamento de itens de sessão a serem convertidos.</param>
        /// <returns></returns>
        public static Dictionary<string, object> AsDictionary(this IEnumerable<IConfigurationSection> sectionItems)
        {
            var dic = new Dictionary<string, object>();
            foreach (var el in sectionItems)
            {
                if (el.Value != null)
                    dic[el.Key] = el.Value;
                else
                    dic[el.Key] = AsDictionary(el.GetChildren());
            }
            return dic;
        }

        /// <summary>
        /// Converter uma sessão de configuração em um dicionário comum.
        /// </summary>
        /// <param name="section">Sessão de configuração a ser convertida.</param>
        /// <returns></returns>
        public static Dictionary<string, object> AsDictionary(this IConfigurationSection section)
        {
            var dic = new Dictionary<string, object>();
            foreach (var el in section.GetChildren())
            {
                if (el.Value != null)
                    dic[el.Key] = el.Value;
                else
                    dic[el.Key] = AsDictionary(el.GetChildren());
            }
            return dic;
        }

    }
}
