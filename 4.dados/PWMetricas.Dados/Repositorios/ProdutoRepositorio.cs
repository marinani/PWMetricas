using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly PwMetricasDbContext _context;

        public ProdutoRepositorio(PwMetricasDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> ObterPorIdAsync(int id)
        {
            return await _context.Produto.FindAsync(id);
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _context.Produto.ToListAsync();
        }

        public async Task AdicionarAsync(Produto produto)
        {
            await _context.Produto.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Produto produto)
        {
            _context.Produto.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var produto = await ObterPorIdAsync(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
