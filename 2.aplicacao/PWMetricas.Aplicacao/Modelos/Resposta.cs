using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos
{
    public class Resposta
    {
        public Resposta(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public Resposta(bool sucesso, string mensagem, object? dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object? Dados { get; set; }
    }
}
