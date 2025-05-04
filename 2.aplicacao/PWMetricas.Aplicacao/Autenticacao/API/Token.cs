using Microsoft.Extensions.Configuration;

namespace PWMetricas.Aplicacao.Autenticacao.API
{
    public class Token : Dictionary<Guid, SystemClaims>
    {
        /// <summary>
        /// Inicializa o controle de token através das configurações informadas.
        /// </summary>
        /// <param name="settings"></param>
        public Token(IConfiguration settings)
        {
            var token = settings.GetSection(nameof(Token)).AsDictionary();
            foreach (var item in token)
            {
                var claimsDic = ((Dictionary<string, object>)item.Value).FirstOrDefault(x => x.Key.Equals("Claims", StringComparison.OrdinalIgnoreCase)).Value;
                var claimsList = ((Dictionary<string, object>)claimsDic).Values;
                var sc = new SystemClaims();
                foreach (var claim in claimsList)
                {
                    var cdic = (Dictionary<string, object>)claim;
                    sc.Add(cdic.First().Key, cdic.First().Value.ToString());
                }
                Add(new Guid(item.Key), sc);
            }

        }
    }
}
