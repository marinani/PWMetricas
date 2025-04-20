using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Perfil;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Aplicacao.Servicos
{
    public class PerfilServico : IPerfilServico
    {
        private readonly IPerfilRepositorio _perfilRepositorio;
        private readonly IMapper _mapper;
        public PerfilServico(IPerfilRepositorio perfilRepositorio, IMapper mapper)
        {
            _perfilRepositorio = perfilRepositorio;
            _mapper = mapper;
        }
        public async Task<PerfilViewModel> ObterPorId(int id)
        {
            var perfil = await _perfilRepositorio.ObterPorIdAsync(id);
            return _mapper.Map<PerfilViewModel>(perfil);
        }
        public async Task<IEnumerable<PerfilViewModel>> ObterTodos()
        {
            var perfis = await _perfilRepositorio.ObterTodosAsync();
            return _mapper.Map<IEnumerable<PerfilViewModel>>(perfis);
        }

        public async Task<Resultado> CadastrarPerfil(PerfilViewModel modelo)
        {
            var resultado = new Resultado();
            try
            {
                // Fix: Initialize all required properties of the 'Perfil' class
                var perfil = new Perfil()
                {
                    Id = 0, // Assuming 0 for new entities; adjust as needed
                    Guid = Guid.NewGuid(), // Generate a new GUID
                    Nome = modelo.Nome,
                    Ativo = true
                };

                await _perfilRepositorio.AdicionarAsync(perfil);

                return new Resultado("Sucesso ao cadastrar item do menu.", perfil);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao cadastrar perfil: " + ex.Message });
            }
        }

        public async Task<PaginacaoResultado<PerfilViewModel>> ObterTodosPaginados(int page, int pageSize)
        {
            var perfis = await _perfilRepositorio.ObterTodosPaginadosAsync(page, pageSize);
            var totalRegistros = await _perfilRepositorio.ContarTotalAsync();

            return new PaginacaoResultado<PerfilViewModel>
            {
                Dados = _mapper.Map<IEnumerable<PerfilViewModel>>(perfis),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                TotalRegistros = totalRegistros
            };
        }

    }
}
