using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Dominio.Filtros;
using PWMetricas.Aplicacao.Modelos.Dashboard;

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
            var model = _mapper.Map<AtendimentoViewModel>(atendimento);

            var lista = await MontarListaObservacoesAtendimento(atendimento.Id);

            model.AtendimentoObservacoes = lista;

            return model;
        }

        public async Task<List<ObservacoesAtendimentoViewModel>> MontarListaObservacoesAtendimento(int atendimentoId)
        {
            var observacoes = await _atendimentoObservacoesRepositorio.ListarObservacoesPorAtendimento(atendimentoId);

            var lista = new List<ObservacoesAtendimentoViewModel>();

            foreach (var item in observacoes)
            {
                lista.Add(new ObservacoesAtendimentoViewModel()
                {
                    Descricao = item.Descricao,
                    Data = item.Data
                }
                );
            }

            return lista;
        }

        public async Task<AtendimentoViewModel> ObterPorGuidComObservacoes(Guid guid)
        {
            var atendimento = await _atendimentoRepositorio.BuscarComObservacoes(guid);
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
                var entidade = await _atendimentoRepositorio.BuscarPorId(modelo.Id);

                entidade.OrigemId = modelo.OrigemId.Value;
                entidade.CanalId = modelo.CanalId.Value;
                entidade.ProdutoId = modelo.ProdutoId.Value;
                entidade.TamanhoId = modelo.TamanhoId.Value;
                entidade.StatusAtendimentoId = modelo.StatusAtendimentoId.Value;
                entidade.ClienteId = modelo.ClienteId.Value;
                entidade.Data = modelo.Data;
                entidade.DataRetorno = modelo.DataRetorno;
                entidade.Whatsapp = modelo.Whatsapp;
                entidade.Uf = modelo.Uf;
                entidade.Cidade = modelo.Cidade;


                await _atendimentoRepositorio.Atualizar(entidade);

                if (!string.IsNullOrEmpty(modelo.Observacao))
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

        public async Task<decimal> SomaTotalAtendimento(int? usuarioId, int? status, int? lojaId)
        {
            var filtro = new AtendimentoFiltro
            {
                LojaId = lojaId,
                UsuarioId = usuarioId,
                StatusAtendimentoId = status
            };

            var soma = await _atendimentoRepositorio.SomaTotal(filtro);
            return soma ?? 0;
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


        public async Task<DashboardVendedorInicial> ObterAtendimentosPorFiltro(AtendimentoFiltro filtro)
        {
            var atendimentos = await _atendimentoRepositorio.ObterAtendimentos(filtro);
            var somaTotal = await _atendimentoRepositorio.SomaTotal(filtro);

            return new DashboardVendedorInicial
            {
                Tarefas = atendimentos.Select(a => new TarefasViewModel
                {
                    Guid = a.Guid,
                    Cliente = a.Cliente.Nome,
                    Status = a.StatusAtendimento.Nome,
                    CorStatusAtendimento = a.StatusAtendimento.CorHex,
                    Data = a.Data.ToShortDateString(),
                    DataRetorno = a.DataRetorno.HasValue ? a.DataRetorno.Value.ToShortDateString() : "",
                    ValorPedido = a.ValorPedido.ToString("C")
                }).ToList(),
                SomaAtendimento = somaTotal.HasValue ? somaTotal.Value : 0,
            };
        }
    }
}
