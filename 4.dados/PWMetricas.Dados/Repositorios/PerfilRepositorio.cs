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
    public class PerfilRepositorio : Repositorio<Perfil>, IPerfilRepositorio
    {
        private readonly PwMetricasDbContext _context;

        public PerfilRepositorio(PwMetricasDbContext contexto) : base(contexto)
        {
            _context = contexto;
        }

        public async Task<Perfil> ObterPorIdAsync(int id)
        {
            return await _context.Perfil.FindAsync(id);
        }

        public async Task<IEnumerable<Perfil>> ObterTodosAsync()
        {
            return await _context.Perfil.ToListAsync();
        }

        public async Task<List<Perfil>> ObterTodosAtivos()
        {
            return await _context.Perfil.Where(x => x.Ativo).OrderBy(X => X.Nome).ToListAsync();

        }
    }
}

