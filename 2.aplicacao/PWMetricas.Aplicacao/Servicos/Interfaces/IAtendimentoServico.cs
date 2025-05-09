﻿using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Dominio.Filtros;
using PWMetricas.Aplicacao.Modelos.Dashboard;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IAtendimentoServico
    {
        Task<Resultado> Cadastrar(AtendimentoViewModel modelo);
        Task<Resultado> Atualizar(AtendimentoViewModel modelo);
        Task<AtendimentoViewModel> ObterPorGuid(Guid guid);
        Task<AtendimentoViewModel> ObterPorGuidComObservacoes(Guid guid);
        Task<decimal> SomaTotalAtendimento(int? usuarioId, int? status, int? lojaId);
        Task<PaginacaoResultado<AtendimentoListaViewModel>> ObterAtendimentosPaginados(int page, int pageSize, AtendimentoFiltro filtro);
        Task<List<ObservacoesAtendimentoViewModel>> MontarListaObservacoesAtendimento(int atendimentoId);
        Task<List<GraficoCoresDto>> ObterOrigemGraficoStatusAsync(int? mes, int? ano, int? loja, int status);
        Task<List<GraficoCoresDto>> ObterCanaisGraficoStatusAsync(int? mes, int? ano, int? loja, int status);
        Task<List<GraficoSimplesDto>> ObterVendedorGraficoStatusAsync(int? mes, int? ano, int? loja, int status);
        Task<List<GraficoSimplesDto>> ObterCidadesGraficoStatusAsync(int? mes, int? ano, int? loja, int status);
        Task<List<TarefasViewModel>> ObterTarefasPorFiltro(AtendimentoFiltro filtro);
        Task<decimal?> ObterSomaTotalAtendimentoPorFiltro(AtendimentoFiltro filtro);

    }
}
