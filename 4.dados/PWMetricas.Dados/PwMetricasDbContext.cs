using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados
{
    public class PwMetricasDbContext : DbContext
    {
        public PwMetricasDbContext(DbContextOptions<PwMetricasDbContext> options)
          : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Loja> Loja { get; set; }
        public DbSet<UsuarioLoja> UsuarioLoja { get; set; }
        public DbSet<StatusAtendimento> StatusAtendimento { get; set; }
        public DbSet<Atendimento> Atendimento { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<AtendimentoObservacoes> AtendimentoObservacoes { get; set; }
        public DbSet<Canal> Canal { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Tamanho> Tamanho { get; set; }
    }
}
