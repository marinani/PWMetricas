using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Dominio.Filtros;
using PWMetricas.Aplicacao.Modelos.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace PWMetricas.Aplicacao.Servicos
{
    public class AtendimentoServico : IAtendimentoServico
    {
        private readonly IAtendimentoRepositorio _atendimentoRepositorio;
        private readonly IOrigemRepositorio _origemRepositorio;
        private readonly ICanalRepositorio _canalRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IAtendimentoObservacoesRepositorio _atendimentoObservacoesRepositorio;
        private readonly IMapper _mapper;
        public AtendimentoServico(IAtendimentoRepositorio atendimentoRepositorio, IMapper mapper,
            IAtendimentoObservacoesRepositorio atendimentoObservacoesRepositorio, IOrigemRepositorio origemRepositorio, ICanalRepositorio canalRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _atendimentoRepositorio = atendimentoRepositorio;
            _mapper = mapper;
            _atendimentoObservacoesRepositorio = atendimentoObservacoesRepositorio;
            _origemRepositorio = origemRepositorio;
            _canalRepositorio = canalRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
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
                    CorStatusAtendimento = a.StatusAtendimento.CorHex,
                    DataRetorno = a.DataRetorno.HasValue ? a.DataRetorno.Value.ToShortDateString() : "",
                    ValorPedido = a.ValorPedido.ToString("C")
                }),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling((double)totalRegistros / pageSize),
                TotalRegistros = totalRegistros,
                SomaTotal = somaTotal.HasValue ? somaTotal.Value.ToString("C") : "0,00",
            };
        }


        public async Task<List<TarefasViewModel>> ObterTarefasPorFiltro(AtendimentoFiltro filtro)
        {
            var atendimentos = await _atendimentoRepositorio.ObterAtendimentos(filtro);

            var listaTarefas = new List<TarefasViewModel>();

            foreach (var item in atendimentos)
            {
                listaTarefas.Add(new TarefasViewModel {
                    Guid = item.Guid,
                    Cliente = item.Cliente.Nome,
                    Status = item.StatusAtendimento.Nome,
                    CorStatusAtendimento = item.StatusAtendimento.CorHex,
                    Data = item.Data.ToShortDateString(),
                    DataRetorno = item.DataRetorno.HasValue ? item.DataRetorno.Value.ToShortDateString() : "",
                    ValorPedido = item.ValorPedido.ToString("C")
                }
                );
            }

            return listaTarefas;
        }

        public async Task<decimal?> ObterSomaTotalAtendimentoPorFiltro(AtendimentoFiltro filtro)
        {
            return await _atendimentoRepositorio.SomaTotal(filtro);
        }



        #region Graficos
        public async Task<List<GraficoCoresDto>> ObterOrigemGraficoStatusAsync(int? mes, int? ano, int? loja, int status)
        {

            var hoje = DateTime.Today;
            var mesVar = hoje.Month;
            var anoVar = hoje.Year;

            if(mes.HasValue && mes.Value > 0)
                mesVar = mes.Value;

            if(ano.HasValue)
                anoVar = ano.Value;


            var primeiroDiaMes = new DateTime(anoVar, mesVar, 1);
            var proximoMes = primeiroDiaMes.AddMonths(1);


            // Filtra os atendimentos válidos do mês atual
            var atendimentosQuery = _atendimentoRepositorio.Listar()
                .Where(a => a.Ativo &&
                            a.Data >= primeiroDiaMes &&
                            a.Data < proximoMes &&
                            a.StatusAtendimentoId == status &&
                            (!loja.HasValue || a.LojaId == loja.Value));

            // Total de atendimentos no mês
            var totalAtendimentos = await atendimentosQuery.CountAsync();

            if (totalAtendimentos == 0)
                return new List<GraficoCoresDto>();

            // Top 4 origens com porcentagem
            var resultado = await atendimentosQuery
                .GroupBy(a => a.OrigemId)
                .Select(grupo => new
                {
                    OrigemId = grupo.Key,
                    Quantidade = grupo.Count()
                })
                .OrderByDescending(x => x.Quantidade)
            //.Take(4)
                .Join(_origemRepositorio.Listar(),
                      atendimento => atendimento.OrigemId,
                      origem => origem.Id,
                      (atendimento, origem) => new GraficoCoresDto
                      {
                          Nome = origem.Nome,
                          Porcentagem = Math.Round((decimal)atendimento.Quantidade * 100 / totalAtendimentos, 2),
                          CorHex = origem.CorHex
                      })
                .ToListAsync();

            return resultado;
        }


        public async Task<List<GraficoCoresDto>> ObterCanaisGraficoStatusAsync(int? mes, int? ano, int? loja, int status)
        {

            var hoje = DateTime.Today;
            var mesVar = hoje.Month;
            var anoVar = hoje.Year;

            if (mes.HasValue && mes.Value > 0)
                mesVar = mes.Value;

            if (ano.HasValue)
                anoVar = ano.Value;


            var primeiroDiaMes = new DateTime(anoVar, mesVar, 1);
            var proximoMes = primeiroDiaMes.AddMonths(1);

            // Filtra os atendimentos válidos do mês atual
            var atendimentosQuery = _atendimentoRepositorio.Listar()
                .Where(a => a.Ativo &&
                            a.Data >= primeiroDiaMes &&
                            a.Data < proximoMes &&
                            a.StatusAtendimentoId == status &&
                            (!loja.HasValue || a.LojaId == loja.Value));

            // Total de atendimentos no mês
            var totalAtendimentos = await atendimentosQuery.CountAsync();

            if (totalAtendimentos == 0)
                return new List<GraficoCoresDto>();

            // Top canais com porcentagem
            var resultado = await atendimentosQuery
                .GroupBy(a => a.CanalId)
                .Select(grupo => new
                {
                    CanalId = grupo.Key,
                    Quantidade = grupo.Count()
                })
                .OrderByDescending(x => x.Quantidade)
                .Join(_canalRepositorio.Listar(),
                      atendimento => atendimento.CanalId,
                      canal => canal.Id,
                      (atendimento, canal) => new GraficoCoresDto
                      {
                          Nome = canal.Nome,
                          Porcentagem = Math.Round((decimal)atendimento.Quantidade * 100 / totalAtendimentos, 2),
                          CorHex = canal.CorHex
                      })
                .ToListAsync();

            return resultado;
        }


        public async Task<List<GraficoSimplesDto>> ObterVendedorGraficoStatusAsync(int? mes, int? ano, int? loja, int status)
        {
            var hoje = DateTime.Today;
            var mesVar = hoje.Month;
            var anoVar = hoje.Year;

            if (mes.HasValue && mes.Value > 0)
                mesVar = mes.Value;

            if (ano.HasValue)
                anoVar = ano.Value;


            var primeiroDiaMes = new DateTime(anoVar, mesVar, 1);
            var proximoMes = primeiroDiaMes.AddMonths(1);

            // Filtra os atendimentos válidos do mês atual
            var atendimentosQuery = _atendimentoRepositorio.Listar()
                .Where(a => a.Ativo &&
                            a.Data >= primeiroDiaMes &&
                            a.Data < proximoMes &&
                            a.StatusAtendimentoId == status &&
                            (!loja.HasValue || a.LojaId == loja.Value));

            // Total de atendimentos no mês
            var totalAtendimentos = await atendimentosQuery.CountAsync();

            if (totalAtendimentos == 0)
                return new List<GraficoSimplesDto>();

            // Top vendedores com porcentagem
            var resultado = await atendimentosQuery
                .GroupBy(a => a.UsuarioId)
                .Select(grupo => new
                {
                    UsuarioId = grupo.Key,
                    Quantidade = grupo.Count()
                })
                .OrderByDescending(x => x.Quantidade)
                .Join(_usuarioRepositorio.Listar(x=> x.PerfilId == 3),
                      atendimento => atendimento.UsuarioId,
                      vendedor => vendedor.Id,
                      (atendimento, vendedor) => new GraficoSimplesDto
                      {
                          Nome = vendedor.Nome,
                          Porcentagem = Math.Round((decimal)atendimento.Quantidade * 100 / totalAtendimentos, 2)
                      })
                .Take(4)
                .ToListAsync();

            return resultado;
        }


        public async Task<List<GraficoSimplesDto>> ObterCidadesGraficoStatusAsync(int? mes, int? ano, int? loja, int status)
        {
            var hoje = DateTime.Today;
            var mesVar = hoje.Month;
            var anoVar = hoje.Year;

            if (mes.HasValue && mes.Value > 0)
                mesVar = mes.Value;

            if (ano.HasValue)
                anoVar = ano.Value;


            var primeiroDiaMes = new DateTime(anoVar, mesVar, 1);
            var proximoMes = primeiroDiaMes.AddMonths(1);

            // Filtra os atendimentos válidos do mês atual
            var atendimentosQuery = _atendimentoRepositorio.Listar()
                .Where(a => a.Ativo &&
                            a.Data >= primeiroDiaMes &&
                            a.Data < proximoMes &&
                            a.StatusAtendimentoId == status &&
                            (!loja.HasValue || a.LojaId == loja.Value));

            // Total de atendimentos no mês
            var totalAtendimentos = await atendimentosQuery.CountAsync();

            if (totalAtendimentos == 0)
                return new List<GraficoSimplesDto>();


            var resultado = await atendimentosQuery
            .GroupBy(a => a.Cidade)
            .Select(grupo => new GraficoSimplesDto
            {
                Nome = grupo.Key,
                Porcentagem = Math.Round((decimal)grupo.Count() * 100 / totalAtendimentos, 2)
            })
           .OrderByDescending(x => x.Porcentagem)
           .Take(4)
           .ToListAsync();

            return resultado;
        }
        #endregion

    }
}
