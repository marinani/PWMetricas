namespace PWMetricas.Aplicacao.Autenticacao.API
{
    /// <summary>
    /// Padronização de retorno para o cliente da API.
    /// </summary>
    public class Resposta<T> : Resposta
    {
        /// <summary>
        /// Conteúdo solicitado.
        /// </summary>
        public T Dados { get; set; }
    }


    /// <summary>
    /// Padronização de retorno para o cliente da API.
    /// </summary>
    public class Resposta
    {
        /// <summary>
        /// Criar uma instância de <see cref="Resposta"/>.
        /// </summary>
        /// <param name="comRastreador">Determina se um identificador será enviado ao cliente para rastreamento de exceção.</param>
        public Resposta(bool comRastreador = false)
        {
            if (comRastreador)
                this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Determina se a resposta produziu resultados (corretamente) ou não.
        /// </summary>
        public bool Sucesso
        {
            get
            {
                return (this.Erros?.Count ?? 0) == 0;
            }
        }

        /// <summary>
        /// Data e horário que foi gerada a resposta, para auxiliar no rastreamento de exceções.
        /// </summary>
        public DateTime Horario { get; set; } = DateTime.Now;

        /// <summary>
        /// Identificador único da resposta, quando exceção fatal.
        /// </summary>
        public Guid? Id { get; private set; }

        /// <summary>
        /// Informações complementares do retorno.
        /// </summary>
        public List<string> Informacoes { get; set; } = new List<string>();

        /// <summary>
        /// Alertas/avisos sobre o retorno. Os alertas completos podem ser obtidos pelo log no serviço.
        /// </summary>
        public List<string> Erros { get; set; } = new List<string>();

        /// <summary>
        /// Remover listas não utilizadas.
        /// </summary>
        public void Limpar()
        {
            if (this.Informacoes?.Count == 0)
                this.Informacoes = null;
            if (this.Erros?.Count == 0)
                this.Erros = null;
        }
    }

}
