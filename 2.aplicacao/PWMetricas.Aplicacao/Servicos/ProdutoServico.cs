
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Aplicacao.Modelos.Produto;
using PWMetricas.Aplicacao.Modelos;
using AutoMapper;
using PWMetricas.Aplicacao.Modelos.Origem;
using PWMetricas.Dados.Repositorios;

namespace PWMetricas.Aplicacao.Servicos
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IMapper _mapper;

        public ProdutoServico(IProdutoRepositorio produtoRepositorio, IMapper mapper)
        {
            _produtoRepositorio = produtoRepositorio;
            _mapper = mapper;
        }

        public async Task<ProdutoViewModel> ObterPorId(int id)
        {
            var origem = await _produtoRepositorio.BuscarPorId(id);
            return _mapper.Map<ProdutoViewModel>(origem);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var perfis = await _produtoRepositorio.ListarAtivosAsync();
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(perfis);
        }

        public async Task<PaginacaoResultado<ProdutoViewModel>> ObterTodosPaginados(int page, int pageSize)
        {
            var perfis = await _produtoRepositorio.ObterTodosPaginados(page, pageSize);
            var totalRegistros = await _produtoRepositorio.ContarTotal();

            return new PaginacaoResultado<ProdutoViewModel>
            {
                Dados = _mapper.Map<IEnumerable<ProdutoViewModel>>(perfis),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                TotalRegistros = totalRegistros
            };
        }

        public async Task<Resultado> Cadastrar(ProdutoViewModel modelo)
        {
            var resultado = new Resultado();


            try
            {
                //if (await _produtoRepositorio.ExisteComTexto(nameof(modelo.Nome), modelo.Nome))
                //    new Resultado(new[] { "Já existe um produto com o mesmo Nome cadastrado " });

                var entidade = new Produto()
                {
                    Id = 0, // Assuming 0 for new entities; adjust as needed
                    Guid = Guid.NewGuid(), // Generate a new GUID
                    Nome = modelo.Nome,
                    CorHex = modelo.CorHex,
                    Ativo = modelo.Ativo
                };
                await _produtoRepositorio.Inserir(entidade);

                var usuarioSalvo = await _produtoRepositorio.BuscarPorId(entidade.Id);

                return new Resultado("Sucesso ao cadastrar produto.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao cadastrar produto: " + ex.Message });
            }
        }

        public async Task<Resultado> Atualizar(ProdutoViewModel modelo)
        {
            var resultado = new Resultado();

            

            var entidade = await _produtoRepositorio.BuscarPorId(modelo.Id);

            if (entidade == null)
            {
                return new Resultado(new[] { "Usuário não encontrado." });
            }

            //if (await _produtoRepositorio.ExisteComTexto(nameof(modelo.Nome), modelo.Nome, entidade.Id))
            //    new Resultado(new[] { "Já existe um produto com o mesmo Nome cadastrado " });

            try
            {

                entidade.Nome = modelo.Nome;
                entidade.CorHex = modelo.CorHex;
                entidade.Ativo = modelo.Ativo;

                await _produtoRepositorio.Atualizar(entidade);


                return new Resultado("Sucesso ao atualizar usuário.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao atualizar usuário: " + ex.Message });
            }
        }

    }
}
