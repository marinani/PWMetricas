using PWMetricas.Aplicacao.Modelos.Cliente;
using PWMetricas.Aplicacao.Modelos;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IClienteServico
    {
        Task<IEnumerable<ClienteSimplesViewModel>> ObterTodos();
        Task<ClienteViewModel> ObterPorGuid(Guid guid);
        Task<Resultado> Cadastrar(ClienteViewModel modelo);
        Task<Resultado> Atualizar(ClienteViewModel modelo);
        Task<PaginacaoResultado<ClienteViewModel>> ObterTodosPaginados(int page, int pageSize);
    }
}
