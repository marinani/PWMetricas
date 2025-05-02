using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Aplicacao.Modelos.Cliente;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Dados.Repositorios;

namespace PWMetricas.Aplicacao.Servicos
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _repositorio;
        private readonly IMapper _mapper;
        public ClienteServico(IClienteRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteSimplesViewModel>> ObterTodos()
        {
            var clientes = await _repositorio.ListarAsync();
            return _mapper.Map<IEnumerable<ClienteSimplesViewModel>>(clientes);
        }

        public async Task<ClienteViewModel> ObterPorGuid(Guid guid)
        {
            var cliente = await _repositorio.Buscar(guid);
            return _mapper.Map<ClienteViewModel>(cliente);
        }

        public async Task<Resultado> Cadastrar(ClienteViewModel modelo)
        {
            var resultado = new Resultado();
            try
            {

                //if (await _repositorio.ExisteComTexto(nameof(modelo.Nome), modelo.Nome))
                //    new Resultado(new[] { "Já existe um cliente com o mesmo Nome cadastrado " });

                var cliente = _mapper.Map<Cliente>(modelo);
                await _repositorio.Inserir(cliente);
                return new Resultado("Sucesso ao cadastrar cliente.", cliente);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { $"Erro ao cadastrar cliente: {ex.Message}" });
            }
        }


        public async Task<Resultado> Atualizar(ClienteViewModel modelo)
        {
            var resultado = new Resultado();


            var cliente = await _repositorio.Buscar(modelo.Guid);

            if (cliente == null)
            {
                return new Resultado(new[] { "Erro ao encontrar cliente." });
            }


            //if (await _repositorio.ExisteComTexto(nameof(modelo.Nome), modelo.Nome, cliente.Id))
            //    new Resultado(new[] { "Já existe um cliente com o mesmo Nome cadastrado " });

            try
            {
                _mapper.Map(modelo, cliente);
                await _repositorio.Atualizar(cliente);
                return new Resultado("Sucesso ao atualizar cliente.", cliente);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { $"Erro ao atualizar cliente: {ex.Message}" });
            }
        }

        public async Task<PaginacaoResultado<ClienteViewModel>> ObterTodosPaginados(int page, int pageSize)
        {
            var perfis = await _repositorio.ObterTodosPaginados(page, pageSize);
            var totalRegistros = await _repositorio.ContarTotal();

            return new PaginacaoResultado<ClienteViewModel>
            {
                Dados = _mapper.Map<IEnumerable<ClienteViewModel>>(perfis),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                TotalRegistros = totalRegistros
            };
        }
    }
    
}
