using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Aplicacao.Modelos.Perfil;
using PWMetricas.Aplicacao.Modelos;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IPerfilServico
    {
        Task<PerfilViewModel> ObterPorId(int id);
        Task<IEnumerable<PerfilViewModel>> ObterTodos();
        Task<Resultado> CadastrarPerfil(PerfilViewModel modelo);
        Task<PaginacaoResultado<PerfilViewModel>> ObterTodosPaginados(int page, int pageSize);
    }
}
