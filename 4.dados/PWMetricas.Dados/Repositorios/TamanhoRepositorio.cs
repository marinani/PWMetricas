using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios
{
    public class TamanhoRepositorio : Repositorio<Tamanho>, ITamanhoRepositorio
    {
        private readonly PwMetricasDbContext _context;
        public TamanhoRepositorio(PwMetricasDbContext contexto) : base(contexto)
        {
            _context = contexto;
        }
        
    }
}
