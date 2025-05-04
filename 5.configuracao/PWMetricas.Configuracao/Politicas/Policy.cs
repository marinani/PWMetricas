using PWMetricas.Aplicacao.Autenticacao.API;
using Microsoft.Extensions.Configuration;

namespace PWMetricas.Configuracao.Politicas
{
    /// <summary>
    /// Políticas de acesso disponíveis.
    /// </summary>
    public partial class Policy
    {
        public static IEnumerable<PolicyClaimValue> Compilar(IConfiguration settings)
        {
            var section = settings.GetSection("Policies").AsDictionary();
            foreach (var item in section)
            {
                string policy = item.Key;
                foreach (var sub in (Dictionary<string, object>)item.Value)
                {
                    foreach (var c in (Dictionary<string, object>)sub.Value)
                    {
                        yield return new PolicyClaimValue()
                        {
                            Policy = policy,
                            ClaimType = c.Key,
                            ClaimValue = c.Value.ToString(),
                        };
                    }
                }
            }
        }

        public struct PolicyClaimValue
        {
            public string Policy;
            public string ClaimType;
            public string ClaimValue;
        }
    }

    public partial class Policy
    {
        public const string Pesquisa = "Pesquisa";

        public const string GetContatos = "GetContatos";

        public const string PostContatos = "PostContatos";
    }

}
