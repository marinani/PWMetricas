using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Loja;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface ILojaServico
    {
        Task<IEnumerable<LojaSimplesViewModel>> ObterTodos();
        Task<LojaViewModel> ObterPorGuid(Guid guid);
        Task<Resultado> Cadastrar(LojaViewModel modelo);
        Task<Resultado> Atualizar(LojaViewModel modelo);
        Task<PaginacaoResultado<LojaViewModel>> ObterTodosPaginados(int page, int pageSize);
    }
}
