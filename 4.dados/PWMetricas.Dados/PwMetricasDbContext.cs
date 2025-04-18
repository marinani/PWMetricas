using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PWMetricas.Dominio.Models;

namespace PWMetricas.Dados
{
    public class PwMetricasDbContext : DbContext
    {
        public PwMetricasDbContext(DbContextOptions<PwMetricasDbContext> options)
          : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
    }
}
