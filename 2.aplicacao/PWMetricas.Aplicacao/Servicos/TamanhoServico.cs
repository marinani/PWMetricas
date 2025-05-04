using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Tamanho;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Aplicacao.Servicos
{
    public class TamanhoServico : ITamanhoServico
    {
        private readonly ITamanhoRepositorio _tamanhoRepositorio;
        private readonly IMapper _mapper;
        public TamanhoServico(ITamanhoRepositorio tamanhoRepositorio, IMapper mapper)
        {
            _tamanhoRepositorio = tamanhoRepositorio;
            _mapper = mapper;
        }

        public async Task<TamanhoViewModel> ObterPorId(int id)
        {
            var tamanho = await _tamanhoRepositorio.BuscarPorId(id);
            return _mapper.Map<TamanhoViewModel>(tamanho);
        }

        public async Task<IEnumerable<TamanhoViewModel>> ObterTodos()
        {
            var perfis = await _tamanhoRepositorio.ListarAtivosAsync();
            return _mapper.Map<IEnumerable<TamanhoViewModel>>(perfis);
        }

        public async Task<PaginacaoResultado<TamanhoViewModel>> ObterTodosPaginados(int page, int pageSize)
        {
            var perfis = await _tamanhoRepositorio.ObterTodosPaginados(page, pageSize);
            var totalRegistros = await _tamanhoRepositorio.ContarTotal();

            return new PaginacaoResultado<TamanhoViewModel>
            {
                Dados = _mapper.Map<IEnumerable<TamanhoViewModel>>(perfis),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                TotalRegistros = totalRegistros
            };
        }

        public async Task<Resultado> Cadastrar(TamanhoViewModel modelo)
        {
            var resultado = new Resultado();

           
            try
            {
                //if (await _tamanhoRepositorio.ExisteComTexto(nameof(modelo.Nome), modelo.Nome))
                //    new Resultado(new[] { "Já existe um tamanho com o mesmo Nome cadastrado " });

                var entidade = new Tamanho()
                {
                    Id = 0, 
                    Guid = Guid.NewGuid(), 
                    Nome = modelo.Nome,
                    CorHex = modelo.CorHex,
                    Ativo = modelo.Ativo
                };

                await _tamanhoRepositorio.Inserir(entidade);

                var usuarioSalvo = await _tamanhoRepositorio.BuscarPorId(entidade.Id);

                return new Resultado("Sucesso ao cadastrar tamanho.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao cadastrar tamanho: " + ex.Message });
            }
        }

        public async Task<Resultado> Atualizar(TamanhoViewModel modelo)
        {
            var resultado = new Resultado();



            var entidade = await _tamanhoRepositorio.BuscarPorId(modelo.Id);

            if (entidade == null)
            {
                return new Resultado(new[] { "Tamanho não encontrado." });
            }

            //if (await _tamanhoRepositorio.ExisteComTexto(nameof(modelo.Nome), modelo.Nome, entidade.Id))
            //    new Resultado(new[] { "Já existe um tamanho com o mesmo Nome cadastrado " });

            try
            {

                entidade.Nome = modelo.Nome;
                entidade.CorHex = modelo.CorHex;
                entidade.Ativo = modelo.Ativo;

                await _tamanhoRepositorio.Atualizar(entidade);


                return new Resultado("Sucesso ao atualizar tamanho.", entidade);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao atualizar tamanho: " + ex.Message });
            }
        }
    }
}
