using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios
{
    public abstract class Repositorio<TEntidade> : IRepositorio<TEntidade> where TEntidade : EntidadeBase
    {
        protected Repositorio(PwMetricasDbContext contexto)
        {
            Contexto = contexto;
            Consulta = Contexto.Set<TEntidade>();
            Lista = Consulta.AsNoTracking().AsQueryable();
        }

        protected PwMetricasDbContext Contexto { get; }
        protected DbSet<TEntidade> Consulta { get; }
        public IQueryable<TEntidade> Lista { get; }

        public async Task<TEntidade> Buscar(int id) =>
            await Consulta.FindAsync(id);

        public async Task<TEntidade> BuscarPorId(int id) =>
            await Consulta.FirstOrDefaultAsync(x => x.Id == id);

        public virtual async Task<TEntidade> Buscar(Guid chave) =>
            await Consulta.Where(x => x.Guid == chave).FirstOrDefaultAsync();

        public async Task<TEntidade> Buscar(Expression<Func<TEntidade, bool>> expressao) =>
            await Consulta.Where(expressao).FirstOrDefaultAsync();

        //public virtual async Task<dynamic> Buscar(Filtro<TEntidade> filtro)
        //{
        //    filtro.PrepararPaginacao();
        //    var consulta = filtro.Consulta();
        //    var total = await Consulta.AsExpandable().AsNoTracking().Where(consulta).CountAsync();
        //    var lista = await Consulta
        //        .AsExpandable()
        //        .AsNoTracking()
        //        .Where(consulta)
        //        .OrderBy($@"{filtro.OrdemPor} {filtro.Ordem}")
        //        .Skip(filtro.Pagina)
        //        .Take(filtro.TamanhoPagina)
        //        .Select(filtro.Propriedades())
        //        .ToListAsync();
        //    return new { Lista = lista, Total = total };
        //}

        public Task<bool> Existe(Expression<Func<TEntidade, bool>> expressao) => Consulta.AnyAsync(expressao);

        public virtual async Task<bool> ExisteComTexto(string nomeColuna, string valor, int? idComparar)
        {
            var consulta =
                Consulta
                    .Where(x => Funcoes.FnRemoverAcento(EF.Property<string>(x, nomeColuna)) == Funcoes.FnRemoverAcento(valor));
            if (idComparar.HasValue)
                consulta = consulta.Where(x => x.Id != idComparar.Value);

            return await consulta.AnyAsync();
        }

        //caso haja a necessidade, o SaveChanges pode ser movido para outro método, permitindo que haja múltiplas alterações no contexto e salvando tudo em apenas um save.
        public async Task Inserir(TEntidade entidade)
        {
            await Consulta.AddAsync(entidade);
            await Contexto.SaveChangesAsync(CancellationToken.None);
        }

        public async Task Inserir(TEntidade[] entidades)
        {
            Consulta.AddRange(entidades);
            await Contexto.SaveChangesAsync(CancellationToken.None);
        }

        public async Task Atualizar(TEntidade entidade)
        {
            Consulta.Update(entidade);
            await Contexto.SaveChangesAsync(CancellationToken.None);
        }

        public bool AtualizarSincrono(TEntidade entidade)
        {
            Consulta.Update(entidade);
            return (Contexto.SaveChanges()) > 0;
        }

        public IQueryable<TEntidade> Listar() =>
                        Contexto.Set<TEntidade>().AsNoTracking();

        public IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> expressao) =>
                Contexto.Set<TEntidade>().Where(expressao).AsNoTracking();

        public async Task<IEnumerable<TEntidade>> ListarAsync() =>
            await Lista.ToListAsync();

        public async Task<IEnumerable<TEntidade>> ListarAsync(Expression<Func<TEntidade, bool>> expressao) =>
            await Lista.Where(expressao).ToListAsync();

        public async Task<TEntidade> InserirERecuperar(TEntidade entidade)
        {
            await Consulta.AddAsync(entidade);
            await Contexto.SaveChangesAsync(CancellationToken.None);

            // Get the inserted entity's identifier
            int id = entidade.Id; // Assuming 'Id' is the property that holds the identifier

            // Retrieve the entity using the identifier
            TEntidade insertedEntity = await RecuperarPorId(id);
            return insertedEntity;
        }

        public async Task<TEntidade> RecuperarPorId(int id)
        {
            return await Contexto.Set<TEntidade>().FindAsync(id);
        }

        public async Task Excluir(int id)
        {
            var registro = await Consulta.FindAsync(id);
            Consulta.Remove(registro!);
            await Contexto.SaveChangesAsync(CancellationToken.None);
        }
    }
}
