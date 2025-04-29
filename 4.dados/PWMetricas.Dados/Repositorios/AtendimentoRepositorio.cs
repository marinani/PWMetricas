using Microsoft.EntityFrameworkCore;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Dominio.Filtros;

namespace PWMetricas.Dados.Repositorios
{
    public class AtendimentoRepositorio : Repositorio<Atendimento>, IAtendimentoRepositorio
    {
        private readonly PwMetricasDbContext _context;

        public AtendimentoRepositorio(PwMetricasDbContext contexto) : base(contexto)
        {
        }

        public async Task<IEnumerable<Atendimento>> ObterAtendimentosPaginados(int page, int pageSize, AtendimentoFiltro filtro)
        {


            var query = Consulta
                .Include(x => x.StatusAtendimento)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Include(x => x.Loja)
                .Include(x => x.Produto)
                .Include(x => x.Tamanho)
                .Include(x => x.Canal)
                .Include(x => x.Origem)
                .AsQueryable();

            if (filtro.StatusAtendimentoId.HasValue)
            {
                query = query.Where(x => x.StatusAtendimentoId == filtro.StatusAtendimentoId.Value);
            }

            if (filtro.UsuarioId.HasValue)
            {
                query = query.Where(x => x.UsuarioId == filtro.UsuarioId.Value);
            }

            if (filtro.LojaId.HasValue)
            {
                query = query.Where(x => x.LojaId == filtro.LojaId.Value);
            }

            if (filtro.DataInicio.HasValue)
            {
                query = query.Where(x => x.Data >= filtro.DataInicio.Value);
            }

            if (filtro.DataFim.HasValue)
            {
                query = query.Where(x => x.Data <= filtro.DataFim.Value);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(x => x.DataRetorno)
                .ToListAsync();
        }

        public async Task<int> ContarAtendimentos(AtendimentoFiltro filtro)
        {
            var query = Consulta.AsQueryable();

            if (filtro.UsuarioId.HasValue)
                query = query.Where(x => x.UsuarioId == filtro.UsuarioId.Value);

            if (filtro.LojaId.HasValue)
                query = query.Where(x => x.LojaId == filtro.LojaId.Value);

            if (filtro.StatusAtendimentoId.HasValue)
                query = query.Where(x => x.StatusAtendimentoId == filtro.StatusAtendimentoId.Value);

            if (filtro.DataInicio.HasValue)
                query = query.Where(x => x.Data >= filtro.DataInicio.Value);

            if (filtro.DataFim.HasValue)
                query = query.Where(x => x.Data <= filtro.DataFim.Value);

            return await query.CountAsync();
        }
    }
}
