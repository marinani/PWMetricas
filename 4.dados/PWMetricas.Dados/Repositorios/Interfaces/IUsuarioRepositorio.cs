using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> ObterPorIdAsync(int id);
        Task<IEnumerable<Usuario>> ObterTodosAsync();
        Task<List<Usuario>> ObterTodosAtivos();
        Task AdicionarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task RemoverAsync(int id);
        Task<IEnumerable<Usuario>> ObterTodosPaginadosAsync(int page, int pageSize);
        Task<int> ContarTotalAsync();
        Task<Usuario?> ObterPorEmailAsync(string email);
    }
}
