﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly PwMetricasDbContext _context;

        public UsuarioRepositorio(PwMetricasDbContext contexto) : base(contexto)
        {
        }
       
        public async Task<IEnumerable<Usuario>> ObterTodosAsync()
        {
            return await Consulta.ToListAsync();
        }

        public async Task<List<Usuario>> ObterTodosAtivos()
        {
            return await Consulta.Where(x => x.Ativo).OrderBy(X => X.Nome).ToListAsync();
        }


        public async Task<IEnumerable<Usuario>> ObterTodosPaginadosAsync(int page, int pageSize)
        {
            return await Consulta
                .Include(x => x.Perfil)
                .OrderBy(p => p.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObterVendedoresPaginadosAsync(int page, int pageSize)
        {
            return await Consulta
                .Include(x => x.Loja)
                .Include(x => x.Perfil)
                .Where(x => x.Perfil.Nome.ToUpper().Contains("VENDEDOR"))
                .OrderBy(p => p.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Usuario> BuscarVendedorPorId(int id) =>
           await Consulta
            .Include(x=> x.Loja)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<int> ContarTotalVendedoresAsync()
        {
            return await Consulta.Where(x=> x.PerfilId == 3).CountAsync();
        }

        public async Task<int> ContarTotalAsync()
        {
            return await Consulta.CountAsync();
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await Consulta
                .Include(x => x.Perfil).FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
