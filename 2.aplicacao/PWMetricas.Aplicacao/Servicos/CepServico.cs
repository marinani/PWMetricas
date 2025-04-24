using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PWMetricas.Aplicacao.Modelos;

namespace PWMetricas.Aplicacao.Servicos
{
    public class CepServico
    {
        private readonly HttpClient _httpClient;

        public CepServico(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CepResponse> ConsultarCepAsync(string cep)
        {
            var url = $"https://brasilapi.com.br/api/cep/v2/{cep}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CepResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return null; // Ou lance uma exceção personalizada
        }
    }
}
