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

        public async Task<Resultado> Cadastrar(AtendimentoViewModel modelo)
        {
            var resultado = new Resultado();
            try
            {
                var entidade = _mapper.Map<Atendimento>(modelo);
                await _atendimentoRepositorio.Inserir(entidade);
                return new Resultado("Sucesso ao cadastrar atendimento.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { $"Erro ao cadastrar atendimento: {ex.Message}" });
            }
        }

        public async Task<PaginacaoResultado<AtendimentoViewModel>> ObterAtendimentosPaginados(int page, int pageSize, AtendimentoFiltro filtro)
        {
            var atendimentos = await _atendimentoRepositorio.ObterAtendimentosPaginados(page, pageSize, filtro);
            var totalRegistros = await _atendimentoRepositorio.ContarAtendimentos(filtro);

            return new PaginacaoResultado<AtendimentoViewModel>
            {
                Dados = atendimentos.Select(a => new AtendimentoViewModel
                {
                    Data = a.Data,
                    ClienteId = a.ClienteId,
                    UsuarioId = a.UsuarioId,
                    LojaId = a.LojaId,
                    StatusAtendimentoId = a.StatusAtendimentoId
                }),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling((double)totalRegistros / pageSize),
                TotalRegistros = totalRegistros
            };
        }
    }
}
