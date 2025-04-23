using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios
{
    public class OrigemRepositorio : Repositorio<Origem>, IOrigemRepositorio
    {
        private readonly PwMetricasDbContext _context;
        public OrigemRepositorio(PwMetricasDbContext contexto) : base(contexto)
        {
            _context = contexto;
        }
    }
}
