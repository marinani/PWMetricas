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
        private readonly IMapper _mapper;
        public AtendimentoServico(IAtendimentoRepositorio atendimentoRepositorio, IMapper mapper)
        {
            _atendimentoRepositorio = atendimentoRepositorio;
            _mapper = mapper;

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
                var atendimento = await _atendimentoRepositorio.InserirERecuperar(entidade);


                //Todo: Criar objeto para inserir observação caso o campo tenha sido preenchido

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
                var entidade = _mapper.Map<Atendimento>(modelo);
                await _atendimentoRepositorio.Atualizar(entidade);
                return new Resultado("Sucesso ao atualizar atendimento.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { $"Erro ao atualizar atendimento: {ex.Message}" });
            }
        }

        public async Task<PaginacaoResultado<AtendimentoListaViewModel>> ObterAtendimentosPaginados(int page, int pageSize, AtendimentoFiltro filtro)
        {
            var atendimentos = await _atendimentoRepositorio.ObterAtendimentosPaginados(page, pageSize, filtro);
            var totalRegistros = await _atendimentoRepositorio.ContarAtendimentos(filtro);

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
                TotalRegistros = totalRegistros
            };
        }
    }
}
