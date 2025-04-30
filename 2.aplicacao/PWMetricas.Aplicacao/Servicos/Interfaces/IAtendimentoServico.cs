using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Dominio.Filtros;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IAtendimentoServico
    {
        Task<Resultado> Cadastrar(AtendimentoViewModel modelo);
        Task<Resultado> Atualizar(AtendimentoViewModel modelo);
        Task<AtendimentoViewModel> ObterPorGuid(Guid guid);
        Task<decimal> SomaTotalAtendimento(int? usuarioId, int? status, int? lojaId);
        Task<PaginacaoResultado<AtendimentoListaViewModel>> ObterAtendimentosPaginados(int page, int pageSize, AtendimentoFiltro filtro);
    }
}
