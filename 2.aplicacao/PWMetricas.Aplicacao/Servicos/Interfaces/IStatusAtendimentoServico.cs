using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.StatusAtendimento;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IStatusAtendimentoServico
    {
        Task<StatusAtendimentoViewModel> ObterPorId(int id);
        Task<PaginacaoResultado<StatusAtendimentoViewModel>> ObterTodosPaginados(int page, int pageSize);
        Task<Resultado> Cadastrar(StatusAtendimentoViewModel modelo);
        Task<Resultado> Atualizar(StatusAtendimentoViewModel modelo);
        Task<IEnumerable<StatusAtendimentoViewModel>> ObterTodos();

    }
}
