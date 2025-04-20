using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios.Interfaces
{
    public interface IPerfilRepositorio
    {
        Task<Perfil> ObterPorIdAsync(int id);
        Task<IEnumerable<Perfil>> ObterTodosAsync();
        Task AdicionarAsync(Perfil perfil);
        Task AtualizarAsync(Perfil perfil);
        Task RemoverAsync(int id);
        Task<IEnumerable<Perfil>> ObterTodosPaginadosAsync(int page, int pageSize);
        Task<int> ContarTotalAsync();
    }
}
