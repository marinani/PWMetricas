using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task<IEnumerable<Usuario>> ObterTodosAsync();
        Task<List<Usuario>> ObterTodosAtivos();
        Task<IEnumerable<Usuario>> ObterTodosPaginadosAsync(int page, int pageSize);
        Task<int> ContarTotalAsync();
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<IEnumerable<Usuario>> ObterVendedoresPaginadosAsync(int page, int pageSize);
        Task<int> ContarTotalVendedoresAsync();
    }
}
