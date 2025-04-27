using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Dominio.Filtros;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IAtendimentoServico
    {
        Task<Resultado> Cadastrar(AtendimentoViewModel modelo);
        Task<PaginacaoResultado<AtendimentoListaViewModel>> ObterAtendimentosPaginados(int page, int pageSize, AtendimentoFiltro filtro);
    }
}
