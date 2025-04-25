using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Aplicacao.Modelos.StatusAtendimento;

namespace PWMetricas.Aplicacao.Servicos
{
    public class StatusAtendimentoServico : IStatusAtendimentoServico
    {
        private readonly IStatusAtendimentoRepositorio _repositorio;
        private readonly IMapper _mapper;

        public string? CorHex { get; private set; }

        public StatusAtendimentoServico(IStatusAtendimentoRepositorio repositorio, IMapper mapper) 
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<StatusAtendimentoViewModel> ObterPorId(int id)
        {
            var origem = await _repositorio.BuscarPorId(id);
            return _mapper.Map<StatusAtendimentoViewModel>(origem);
        }
        public async Task<IEnumerable<StatusAtendimentoViewModel>> ObterTodos()
        {
            var perfis = await _repositorio.ListarAsync();
            return _mapper.Map<IEnumerable<StatusAtendimentoViewModel>>(perfis);
        }

        public async Task<PaginacaoResultado<StatusAtendimentoViewModel>> ObterTodosPaginados(int page, int pageSize)
        {
            var perfis = await _repositorio.ObterTodosPaginados(page, pageSize);
            var totalRegistros = await _repositorio.ContarTotal();

            return new PaginacaoResultado<StatusAtendimentoViewModel>
            {
                Dados = _mapper.Map<IEnumerable<StatusAtendimentoViewModel>>(perfis),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                TotalRegistros = totalRegistros
            };
        }

        public async Task<Resultado> Cadastrar(StatusAtendimentoViewModel modelo)
        {
            var resultado = new Resultado();


            try
            {

                var entidade = new StatusAtendimento() 
                {   Ativo = true, 
                    Guid = Guid.NewGuid(),
                    Id = 0, 
                    Nome = modelo.Nome,
                    CorHex = modelo.CorHex };

                await _repositorio.Inserir(entidade);

                var usuarioSalvo = await _repositorio.BuscarPorId(entidade.Id);

                return new Resultado("Sucesso ao cadastrar status.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao cadastrar status: " + ex.Message });
            }
        }

        public async Task<Resultado> Atualizar(StatusAtendimentoViewModel modelo)
        {
            var resultado = new Resultado();



            var usuario = await _repositorio.BuscarPorId(modelo.Id);

            if (usuario == null)
            {
                return new Resultado(new[] { "Status não encontrado." });
            }

            try
            {

                usuario.Nome = modelo.Nome;
                usuario.CorHex = modelo.CorHex;
                usuario.Ativo = modelo.Ativo;

                await _repositorio.Atualizar(usuario);


                return new Resultado("Sucesso ao atualizar status.", usuario);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao atualizar status: " + ex.Message });
            }
        }
    }
}
