using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios.Interfaces
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        Task<Produto> ObterPorIdAsync(int id);
        Task<IEnumerable<Produto>> ObterTodosAsync();
       
    }
}
