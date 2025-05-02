using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Loja;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Aplicacao.Servicos
{
    public class LojaServico : ILojaServico
    {
        private readonly ILojaRepositorio _repositorio;
        private readonly IMapper _mapper;
        public LojaServico(ILojaRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LojaSimplesViewModel>> ObterTodos()
        {
            var perfis = await _repositorio.ListarAsync();
            return _mapper.Map<List<LojaSimplesViewModel>>(perfis);
        }

        public async Task<LojaViewModel> ObterPorGuid(Guid guid)
        {
            var cliente = await _repositorio.Buscar(guid);
            return _mapper.Map<LojaViewModel>(cliente);
        }

        public async Task<Resultado> Cadastrar(LojaViewModel modelo)
        {
            var resultado = new Resultado();
            try
            {

                if (await _repositorio.ExisteComTexto(nameof(modelo.NomeFantasia), modelo.NomeFantasia))
                    new Resultado(new[] { "Já existe uma loja com o mesmo Nome Fantasia cadastrado " });

                var loja = _mapper.Map<Loja>(modelo);
                await _repositorio.Inserir(loja);
                return new Resultado("Sucesso ao cadastrar loja.", loja);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { $"Erro ao cadastrar loja: {ex.Message}" });
            }
        }

        public async Task<Resultado> Atualizar(LojaViewModel modelo)
        {
            var resultado = new Resultado();


            var loja = await _repositorio.Buscar(modelo.Guid);

            if (loja == null)
            {
                return new Resultado(new[] { "Erro ao encontrar loja." });
            }

            if (await _repositorio.ExisteComTexto(nameof(modelo.NomeFantasia), modelo.NomeFantasia, loja.Id))
                new Resultado(new[] { "Já existe uma loja com o mesmo Nome Fantasia cadastrado " });

            try
            {
                _mapper.Map(modelo, loja);
                await _repositorio.Atualizar(loja);
                return new Resultado("Sucesso ao atualizar loja.", loja);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { $"Erro ao atualizar loja: {ex.Message}" });
            }
        }

        public async Task<PaginacaoResultado<LojaViewModel>> ObterTodosPaginados(int page, int pageSize)
        {
            var perfis = await _repositorio.ObterTodosPaginados(page, pageSize);
            var totalRegistros = await _repositorio.ContarTotal();

            return new PaginacaoResultado<LojaViewModel>
            {
                Dados = _mapper.Map<IEnumerable<LojaViewModel>>(perfis),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                TotalRegistros = totalRegistros
            };
        }
    }
}
