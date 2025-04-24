using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios
{
    public class LojaRepositorio : Repositorio<Loja>, ILojaRepositorio
    {
        private readonly PwMetricasDbContext _context;
        public LojaRepositorio(PwMetricasDbContext contexto) : base(contexto)
        {
            _context = contexto;
        }

    }
}
