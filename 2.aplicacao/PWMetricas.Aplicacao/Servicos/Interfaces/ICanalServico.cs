using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Canal;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface ICanalServico
    {
        Task<CanalViewModel> ObterPorId(int id);
        Task<PaginacaoResultado<CanalViewModel>> ObterTodosPaginados(int page, int pageSize);
        Task<Resultado> Cadastrar(CanalViewModel modelo);
        Task<Resultado> Atualizar(CanalViewModel modelo);
        Task<IEnumerable<CanalViewModel>> ObterTodos();
    }
}
