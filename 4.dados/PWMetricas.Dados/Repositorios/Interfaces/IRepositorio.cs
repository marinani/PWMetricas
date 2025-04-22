using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios.Interfaces
{
    public interface IRepositorio<TEntidade> where TEntidade : EntidadeBase
    {
        /// <summary>
        ///   Busca uma entidade por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntidade> Buscar(int id);

        /// <summary>
        /// Busca uma entidade por uma chave
        /// </summary>
        /// <param name="chave"></param>
        /// <returns></returns>
        Task<TEntidade> Buscar(Guid chave);

        Task<TEntidade> BuscarPorId(int id);


        /// <summary>
        /// Busca uma entidade por uma expressão usando FirstOrDefault
        /// </summary>
        /// <param name="expressao"></param>
        /// <returns></returns>
        Task<TEntidade> Buscar(Expression<Func<TEntidade, bool>> expressao);

        /// <summary>
        /// Realiza uma busca paginada, retornando um objeto contendo o total de registros encontrados e uma
        /// lista com o resultado de acordo com a página e as propriedades selecionadas no filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>{total: int, lista: ienumerable<objeto>}</returns>
        //Task<dynamic> Buscar(Filtro<TEntidade> filtro);

        /// <summary>
        ///   Verifica se um objeto existe na base a partir da expressão.
        ///   Utiliza o .Any() do IQueryable.
        /// </summary>
        /// <param name="expressao"></param>
        /// <returns></returns>
        Task<bool> Existe(Expression<Func<TEntidade, bool>> expressao);

        /// <summary>
        ///
        /// </summary>
        /// <param name="nomeColuna">Nome da properidade, usar nameof(entidade.propriedade)</param>
        /// <param name="valor">Valor para ser comparado</param>
        /// <param name="compararId"></param>
        /// <param name="validarMesmoId">Se true, verifica se x.Id != entidade.Id - usado para validação nas Alterações</param>
        /// <returns></returns>
        Task<bool> ExisteComTexto(string nomeColuna, string valor, int? idComparar = null);
        IQueryable<TEntidade> Listar();
        IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> expressao);
        Task<IEnumerable<TEntidade>> ListarAsync(Expression<Func<TEntidade, bool>> expressao);
        Task<IEnumerable<TEntidade>> ListarAsync();
        Task Inserir(TEntidade entidade);
        Task Inserir(TEntidade[] entidades);
        Task Atualizar(TEntidade entidade);
        Task<TEntidade> InserirERecuperar(TEntidade entidade);
        Task<TEntidade> RecuperarPorId(int id);
        Task Excluir(int id);
        bool AtualizarSincrono(TEntidade entidade);
    }

}
