using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Aplicacao.Servicos
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoServico(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<Produto> ObterPorIdAsync(int id)
        {
            return await _produtoRepositorio.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _produtoRepositorio.ObterTodosAsync();
        }

        public async Task AdicionarAsync(Produto produto)
        {
            await _produtoRepositorio.AdicionarAsync(produto);
        }

        public async Task AtualizarAsync(Produto produto)
        {
            await _produtoRepositorio.AtualizarAsync(produto);
        }

        public async Task RemoverAsync(int id)
        {
            await _produtoRepositorio.RemoverAsync(id);
        }
    }
}
