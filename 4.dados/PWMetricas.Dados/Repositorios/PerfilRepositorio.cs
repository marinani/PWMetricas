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
    public class PerfilRepositorio : IPerfilRepositorio
    {
        private readonly PwMetricasDbContext _context;

        public PerfilRepositorio(PwMetricasDbContext context)
        {
            _context = context;
        }

        public async Task<Perfil> ObterPorIdAsync(int id)
        {
            return await _context.Perfil.FindAsync(id);
        }

        public async Task<IEnumerable<Perfil>> ObterTodosAsync()
        {
            return await _context.Perfil.ToListAsync();
        }

        public async Task AdicionarAsync(Perfil perfil)
        {
            await _context.Perfil.AddAsync(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Perfil perfil)
        {
            _context.Perfil.Update(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var perfil = await ObterPorIdAsync(id);
            if (perfil != null)
            {
                _context.Perfil.Remove(perfil);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Perfil>> ObterTodosPaginadosAsync(int page, int pageSize)
        {
            return await _context.Perfil
                .OrderBy(p => p.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarTotalAsync()
        {
            return await _context.Perfil.CountAsync();
        }
    }
}
