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
            var canais = await _repositorio.ListarAtivosAsync();
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
                //if (await _repositorio.ExisteComTexto(nameof(modelo.Nome), modelo.Nome))
                //    new Resultado(new[] { "Já existe uma canal com o mesmo Nome cadastrado " });

                var entidade = new Canal()
                {
                    Id = 0, // Assuming 0 for new entities; adjust as needed
                    Guid = Guid.NewGuid(), // Generate a new GUID
                    Nome = modelo.Nome,
                    CorHex = modelo.CorHex,
                    Ativo = modelo.Ativo
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



            var entidade = await _repositorio.BuscarPorId(modelo.Id);

            if (entidade == null)
            {
                return new Resultado(new[] { "Canal não encontrado." });
            }

            //if (await _repositorio.ExisteComTexto(nameof(modelo.Nome), modelo.Nome, entidade.Id))
            //    new Resultado(new[] { "Já existe uma canal com o mesmo Nome cadastrado " });

            try
            {

                entidade.Nome = modelo.Nome;
                entidade.CorHex = modelo.CorHex;
                entidade.Ativo = modelo.Ativo;

                await _repositorio.Atualizar(entidade);


                return new Resultado("Sucesso ao atualizar canal.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao atualizar canal: " + ex.Message });
            }
        }
    }
}
