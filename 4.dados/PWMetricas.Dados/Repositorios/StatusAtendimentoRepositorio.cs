using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios
{
    public class StatusAtendimentoRepositorio : Repositorio<StatusAtendimento>, IStatusAtendimentoRepositorio
    {
        private readonly PwMetricasDbContext _context;
        public StatusAtendimentoRepositorio(PwMetricasDbContext contexto) : base(contexto)
        {
            _context = contexto;
        }
    }
}
