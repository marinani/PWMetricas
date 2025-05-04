namespace PWMetricas.Aplicacao.Autenticacao.API
{
    public class Autorizacao
    {
        /// <summary>
        /// Token JWT contendo dados de autenticação e autorização do usuário.
        /// </summary>
        public string TokenAcesso { get; set; }

        /// <summary>
        /// Validade final da sessão do usuário.
        /// </summary>
        public DateTime ValidoAte { get; set; }
    }

}
