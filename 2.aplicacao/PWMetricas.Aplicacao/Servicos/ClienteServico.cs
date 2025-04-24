using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PWMetricas.Aplicacao.Modelos.Usuario;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Aplicacao.Modelos.Cliente;
using PWMetricas.Dominio.Entidades;

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

        public async Task<ClienteViewModel> ObterPorGuid(Guid guid)
        {
            var cliente = await _repositorio.Buscar(guid);
            return _mapper.Map<ClienteViewModel>(cliente);
        }

        public async Task<Resultado> Cadastrar(ClienteViewModel modelo)
        {
            var resultado = new Resultado();
            if (string.IsNullOrEmpty(modelo.Nome))
            {
                return new Resultado(new[] { "O campo nome é obrigatório." });
            }
            try
            {
                var cliente = _mapper.Map<Cliente>(modelo);
                await _repositorio.Inserir(cliente);
                return new Resultado("Sucesso ao cadastrar cliente.", cliente);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { $"Erro ao cadastrar cliente: {ex.Message}" });
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
