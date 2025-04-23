using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Origem;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IOrigemServico
    {
        Task<OrigemViewModel> ObterPorId(int id);
        Task<PaginacaoResultado<OrigemViewModel>> ObterTodosPaginados(int page, int pageSize);
        Task<Resultado> Cadastrar(OrigemViewModel modelo);
        Task<Resultado> Atualizar(OrigemViewModel modelo);

    }
}
