using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<Produto> ObterPorIdAsync(int id);
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverAsync(int id);
    }
}
