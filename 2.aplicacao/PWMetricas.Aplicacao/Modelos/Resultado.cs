using Flunt.Notifications;

namespace PWMetricas.Aplicacao.Modelos
{
    public sealed class Resultado
    {
        private readonly ICollection<string> erros;

        public Resultado() => erros = new List<string>();

        public Resultado(IEnumerable<Notification> erros) =>
            this.erros = new List<string>(erros.Select(x => x.Message));

        public Resultado(string mensagemSucesso)
        {
            MensagemSucesso = mensagemSucesso;
            erros = new List<string>();
        }
        public Resultado(string mensagemSucesso, dynamic? dados)
        {
            MensagemSucesso = mensagemSucesso;
            Dados = dados;
            erros = new List<string>();
        }

        public Resultado(IEnumerable<string> erros) =>
            this.erros = erros?.ToList() ?? new List<string>();

        public Resultado(IEnumerable<string> erros, dynamic? dados)
        {
            this.erros = erros?.ToList() ?? new List<string>();
            Dados = dados;
        }

        public IReadOnlyCollection<string> Erros => erros.ToList();

        public bool Sucesso => !Erros.Any();

        public string MsgRetornoSucesso { get; } = string.Empty;
        public string MensagemSucesso { get; }

        public dynamic? Dados { get; }
    }
}
