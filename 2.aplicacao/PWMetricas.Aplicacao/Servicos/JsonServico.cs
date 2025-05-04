using System.Text.Json;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Aplicacao.Servicos
{
    public class JsonServico : IJsonServico
    {

        public string Serializar(object dados) => JsonSerializer.Serialize(value: dados);

        public T Deserializar<T>(string json)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true

                };
                return JsonSerializer.Deserialize<T>(json, options);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
