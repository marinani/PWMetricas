using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IJsonServico
    {
        /// <summary>
        /// Deserializa os dados retornados em uma resposta de requisição HTTP.
        /// </summary>
        /// <typeparam name="T">Tipo de retorno esperado.</typeparam>
        /// <param name="json">String com JSON para deserialização.</param>
        /// <returns>Dados deserializados.</returns>
        T Deserializar<T>(string json);

        /// <summary>
        /// Serializa os dados para uma string em formato JSON.
        /// </summary>
        /// <param name="dados">Dados para serialização.</param>
        /// <returns>String com dados serializados.</returns>
        string Serializar(object dados);
    }

}
