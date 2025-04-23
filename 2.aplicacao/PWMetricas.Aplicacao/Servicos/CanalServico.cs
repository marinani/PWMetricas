using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Canal;
using PWMetricas.Aplicacao.Modelos.Origem;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Aplicacao.Servicos
{
    public class CanalServico : ICanalServico
    {
        private readonly ICanalRepositorio _repositorio;
        private readonly IMapper _mapper;
        public CanalServico(ICanalRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<CanalViewModel> ObterPorId(int id)
        {
            var canal = await _repositorio.BuscarPorId(id);
            return _mapper.Map<CanalViewModel>(canal);
        }

        public async Task<IEnumerable<CanalViewModel>> ObterTodos()
        {
            var canais = await _repositorio.ListarAsync();
            return _mapper.Map<IEnumerable<CanalViewModel>>(canais);
        }

        public async Task<PaginacaoResultado<CanalViewModel>> ObterTodosPaginados(int page, int pageSize)
        {
            var perfis = await _repositorio.ObterTodosPaginados(page, pageSize);
            var totalRegistros = await _repositorio.ContarTotal();

            return new PaginacaoResultado<CanalViewModel>
            {
                Dados = _mapper.Map<IEnumerable<CanalViewModel>>(perfis),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                TotalRegistros = totalRegistros
            };
        }

        public async Task<Resultado> Cadastrar(CanalViewModel modelo)
        {
            var resultado = new Resultado();


            try
            {

                var entidade = new Canal()
                {
                    Id = 0, // Assuming 0 for new entities; adjust as needed
                    Guid = Guid.NewGuid(), // Generate a new GUID
                    Nome = modelo.Nome,
                    Ativo = true
                };

                await _repositorio.Inserir(entidade);

                var usuarioSalvo = await _repositorio.BuscarPorId(entidade.Id);

                return new Resultado("Sucesso ao cadastrar canal.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao cadastrar canal: " + ex.Message });
            }
        }

        public async Task<Resultado> Atualizar(CanalViewModel modelo)
        {
            var resultado = new Resultado();



            var usuario = await _repositorio.BuscarPorId(modelo.Id);

            if (usuario == null)
            {
                return new Resultado(new[] { "Canal não encontrado." });
            }

            try
            {

                usuario.Nome = modelo.Nome;
                //usuario.Ativo = modelo.Ativo;

                await _repositorio.Atualizar(usuario);


                return new Resultado("Sucesso ao atualizar canal.", usuario);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao atualizar canal: " + ex.Message });
            }
        }
    }
}
