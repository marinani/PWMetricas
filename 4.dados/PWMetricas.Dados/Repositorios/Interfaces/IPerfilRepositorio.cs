using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios.Interfaces
{
    public interface IPerfilRepositorio : IRepositorio<Perfil>
    {
        Task<Perfil> ObterPorIdAsync(int id);
        Task<IEnumerable<Perfil>> ObterTodosAsync();
        Task<List<Perfil>> ObterTodosAtivos();
    }
}
