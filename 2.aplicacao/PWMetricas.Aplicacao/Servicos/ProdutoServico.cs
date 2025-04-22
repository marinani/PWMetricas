using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Aplicacao.Modelos.Tamanho;
using PWMetricas.Aplicacao.Modelos.Produto;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Dados.Repositorios;
using PWMetricas.Aplicacao.Modelos.Usuario;
using AutoMapper;

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

        public async Task<Produto> ObterPorId(int id)
        {
            return await _produtoRepositorio.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _produtoRepositorio.ObterTodosAsync();
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

                var entidade = new Produto()
                {
                    Id = 0, // Assuming 0 for new entities; adjust as needed
                    Guid = Guid.NewGuid(), // Generate a new GUID
                    Nome = modelo.Nome,
                    Ativo = true
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

            

            var usuario = await _produtoRepositorio.BuscarPorId(modelo.Id);

            if (usuario == null)
            {
                return new Resultado(new[] { "Usuário não encontrado." });
            }

            try
            {

                usuario.Nome = modelo.Nome;
                //usuario.Ativo = modelo.Ativo;

                await _produtoRepositorio.Atualizar(usuario);


                return new Resultado("Sucesso ao atualizar usuário.", usuario);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao atualizar usuário: " + ex.Message });
            }
        }

    }
}
