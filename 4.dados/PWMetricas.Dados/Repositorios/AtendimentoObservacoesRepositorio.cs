using Microsoft.EntityFrameworkCore;
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

        public async Task<List<AtendimentoObservacoes>> ListarObservacoesPorAtendimento(int atendimentoId)
        {
            var observacoes = Consulta
                .Where(x => x.AtendimentoId == atendimentoId)
                .OrderByDescending(x=> x.Data).ToListAsync();

            return await observacoes;
        }
    }
}
