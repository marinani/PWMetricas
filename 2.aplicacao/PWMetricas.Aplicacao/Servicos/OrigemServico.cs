using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Aplicacao.Modelos.Origem;

namespace PWMetricas.Aplicacao.Servicos
{
    public class OrigemServico : IOrigemServico
    {
        private readonly IOrigemRepositorio _repositorio;
        private readonly IMapper _mapper;

        public string? CorHex { get; private set; }

        public OrigemServico(IOrigemRepositorio repositorio, IMapper mapper) 
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<OrigemViewModel> ObterPorId(int id)
        {
            var origem = await _repositorio.BuscarPorId(id);
            return _mapper.Map<OrigemViewModel>(origem);
        }
        public async Task<IEnumerable<OrigemViewModel>> ObterTodos()
        {
            var perfis = await _repositorio.ListarAsync();
            return _mapper.Map<IEnumerable<OrigemViewModel>>(perfis);
        }

        public async Task<PaginacaoResultado<OrigemViewModel>> ObterTodosPaginados(int page, int pageSize)
        {
            var perfis = await _repositorio.ObterTodosPaginados(page, pageSize);
            var totalRegistros = await _repositorio.ContarTotal();

            return new PaginacaoResultado<OrigemViewModel>
            {
                Dados = _mapper.Map<IEnumerable<OrigemViewModel>>(perfis),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                TotalRegistros = totalRegistros
            };
        }

        public async Task<Resultado> Cadastrar(OrigemViewModel modelo)
        {
            var resultado = new Resultado();


            try
            {

                var entidade = new Origem() 
                {   Ativo = true, 
                    Guid = Guid.NewGuid(),
                    Id = 0, 
                    Nome = modelo.Nome,
                    CorHex = modelo.CorHex };

                await _repositorio.Inserir(entidade);

                var usuarioSalvo = await _repositorio.BuscarPorId(entidade.Id);

                return new Resultado("Sucesso ao cadastrar origem.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao cadastrar origem: " + ex.Message });
            }
        }

        public async Task<Resultado> Atualizar(OrigemViewModel modelo)
        {
            var resultado = new Resultado();



            var usuario = await _repositorio.BuscarPorId(modelo.Id);

            if (usuario == null)
            {
                return new Resultado(new[] { "Origem não encontrado." });
            }

            try
            {

                usuario.Nome = modelo.Nome;
                usuario.CorHex = modelo.CorHex;
                //usuario.Ativo = modelo.Ativo;

                await _repositorio.Atualizar(usuario);


                return new Resultado("Sucesso ao atualizar origem.", usuario);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao atualizar origem: " + ex.Message });
            }
        }
    }
}
