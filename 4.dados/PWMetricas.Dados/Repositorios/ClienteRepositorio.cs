using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        private readonly PwMetricasDbContext _context;
        public ClienteRepositorio(PwMetricasDbContext contexto) : base(contexto)
        {
            _context = contexto;
        }

    }
}
