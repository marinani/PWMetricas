using Microsoft.EntityFrameworkCore;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios
{
    public class AtendimentoRepositorio : Repositorio<Atendimento>, IAtendimentoRepositorio
    {
        private readonly PwMetricasDbContext _context;

        public AtendimentoRepositorio(PwMetricasDbContext contexto) : base(contexto)
        {
        }

        public async Task<IEnumerable<Atendimento>> ObterEmAtendimentoPaginados(int page, int pageSize)
        {
            return await Consulta
                .Include(x => x.StatusAtendimento)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Include(x => x.Loja)
                .Include(x => x.Produto)
                .Include(x => x.Tamanho)
                .Include(x => x.Canal)
                .Include(x => x.Origem)
                .Where(x => x.StatusAtendimento.Nome.Equals("EM ATENDIMENTO"))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Atendimento>> ObterEmOrcamentoPaginados(int page, int pageSize)
        {
            return await Consulta
                .Include(x => x.StatusAtendimento)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Include(x => x.Loja)
                .Include(x => x.Produto)
                .Include(x => x.Tamanho)
                .Include(x => x.Canal)
                .Include(x => x.Origem)
                .Where(x => x.StatusAtendimento.Nome.Equals("ORÇAMENTO"))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<IEnumerable<Atendimento>> ObterVendidoPaginados(int page, int pageSize)
        {
            return await Consulta
                .Include(x => x.StatusAtendimento)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Include(x => x.Loja)
                .Include(x => x.Produto)
                .Include(x => x.Tamanho)
                .Include(x => x.Canal)
                .Include(x => x.Origem)
                .Where(x => x.StatusAtendimento.Nome.Equals("VENDIDO"))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
