using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Usuario;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IUsuarioServico
    {
        Task<UsuarioViewModel> ObterPorId(int id);
        Task<IEnumerable<UsuarioViewModel>> ObterTodos();
        Task<List<UsuarioViewModel>> ListarAtivos();
        Task<Resultado> CadastrarUsuario(UsuarioViewModel modelo);
        Task<Resultado> EditarUsuario(UsuarioViewModel modelo);
        Task<PaginacaoResultado<UsuarioViewModel>> ObterTodosPaginados(int page, int pageSize);

        Task<UsuarioViewModel?> AutenticarUsuario(string email, string senha);
    }
}
