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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly PwMetricasDbContext _context;

        public UsuarioRepositorio(PwMetricasDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorIdAsync(int id)
        {
            return await _context.Usuario
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Usuario>> ObterTodosAsync()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task<List<Usuario>> ObterTodosAtivos()
        {
            return await _context.Usuario.Where(x => x.Ativo).OrderBy(X => X.Nome).ToListAsync();
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            try
             {
                await _context.Usuario.AddAsync(usuario);
                Console.WriteLine($"Estado da entidade: {_context.Entry(usuario).State}");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar usuário: {ex.Message}");
                throw;
            }

           
        }

        public async Task AtualizarAsync(Usuario perfil)
        {
            _context.Usuario.Update(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var perfil = await ObterPorIdAsync(id);
            if (perfil != null)
            {
                _context.Usuario.Remove(perfil);
                await _context.SaveChangesAsync();
            }
        }



        public async Task<IEnumerable<Usuario>> ObterTodosPaginadosAsync(int page, int pageSize)
        {
            return await _context.Usuario
                .OrderBy(p => p.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarTotalAsync()
        {
            return await _context.Usuario.CountAsync();
        }
    }
}
