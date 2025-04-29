using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios
{
    public class AtendimentoObservacoesRepositorio : Repositorio<AtendimentoObservacoes>, IAtendimentoObservacoesRepositorio
    {
        private readonly PwMetricasDbContext _context;

        public AtendimentoObservacoesRepositorio(PwMetricasDbContext contexto) : base(contexto)
        {
        }
    }
}
