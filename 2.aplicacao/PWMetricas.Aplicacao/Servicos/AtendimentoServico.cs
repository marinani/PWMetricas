using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Dominio.Filtros;

namespace PWMetricas.Aplicacao.Servicos
{
    public class AtendimentoServico : IAtendimentoServico
    {
        private readonly IAtendimentoRepositorio _atendimentoRepositorio;
        private readonly IAtendimentoObservacoesRepositorio _atendimentoObservacoesRepositorio;
        private readonly IMapper _mapper;
        public AtendimentoServico(IAtendimentoRepositorio atendimentoRepositorio, IMapper mapper, IAtendimentoObservacoesRepositorio atendimentoObservacoesRepositorio)
        {
            _atendimentoRepositorio = atendimentoRepositorio;
            _mapper = mapper;
            _atendimentoObservacoesRepositorio = atendimentoObservacoesRepositorio;
        }

        public async Task<AtendimentoViewModel> ObterPorGuid(Guid guid)
        {
            var atendimento = await _atendimentoRepositorio.Buscar(guid);
            return _mapper.Map<AtendimentoViewModel>(atendimento);
        }

        public async Task<Resultado> Cadastrar(AtendimentoViewModel modelo)
        {
            var resultado = new Resultado();
            try
            {
                var entidade = _mapper.Map<Atendimento>(modelo);
                entidade = await _atendimentoRepositorio.InserirERecuperar(entidade);

                if (!string.IsNullOrEmpty(modelo.Observacao))
                {
                    await CriarObservacao(entidade, modelo.Observacao);
                }

                return new Resultado("Sucesso ao cadastrar atendimento.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { $"Erro ao cadastrar atendimento: {ex.Message}" });
            }
        }

        public async Task<Resultado> Atualizar(AtendimentoViewModel modelo)
        {
            var resultado = new Resultado();
            try
            {
                var entidade = await _atendimentoRepositorio.Buscar(modelo.Guid);

                entidade = _mapper.Map<Atendimento>(modelo);
                await _atendimentoRepositorio.Atualizar(entidade);

                if(!string.IsNullOrEmpty(modelo.Observacao))
                {
                    await CriarObservacao(entidade, modelo.Observacao);
                }

                return new Resultado("Sucesso ao atualizar atendimento.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { $"Erro ao atualizar atendimento: {ex.Message}" });
            }
        }

        public async Task<Resultado> CriarObservacao(Atendimento atendimento, string observacao)
        {
            var resultado = new Resultado();
            try
            {
                var entidade = new AtendimentoObservacoes()
                {
                    Id = 0,
                    Guid = Guid.NewGuid(),
                    Data = DateTime.Now,
                    AtendimentoId = atendimento.Id,
                    Descricao = observacao,
                    Ativo = true
                };
                // Verifica se o atendimento existe
                entidade = await _atendimentoObservacoesRepositorio.InserirERecuperar(entidade);

                return new Resultado("Sucesso ao criar observação do atendimento.", entidade);

            }
            catch (Exception ex)
            {
                return new Resultado(new[] { $"Erro ao criar observação: {ex.Message}" });
            }
        }

        public async Task<PaginacaoResultado<AtendimentoListaViewModel>> ObterAtendimentosPaginados(int page, int pageSize, AtendimentoFiltro filtro)
        {
            var atendimentos = await _atendimentoRepositorio.ObterAtendimentosPaginados(page, pageSize, filtro);
            var totalRegistros = await _atendimentoRepositorio.ContarAtendimentos(filtro);
            var somaTotal = await _atendimentoRepositorio.SomaTotal(filtro);

            return new PaginacaoResultado<AtendimentoListaViewModel>
            {
                Dados = atendimentos.Select(a => new AtendimentoListaViewModel
                {
                    Guid = a.Guid,
                    Cliente = a.Cliente.Nome,
                    Status = a.StatusAtendimento.Nome,
                    Data = a.Data.ToShortDateString(),
                    DataRetorno = a.DataRetorno.HasValue ? a.DataRetorno.Value.ToShortDateString() : "",
                    ValorPedido = a.ValorPedido.ToString("C")
                }),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling((double)totalRegistros / pageSize),
                TotalRegistros = totalRegistros,
                SomaTotal = somaTotal.HasValue ? somaTotal.Value.ToString("C") : "0,00",
            };
        }
    }
}
