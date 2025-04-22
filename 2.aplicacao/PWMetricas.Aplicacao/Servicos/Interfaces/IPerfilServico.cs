using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Aplicacao.Modelos.Perfil;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Tamanho;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IPerfilServico
    {
        Task<PerfilViewModel> ObterPorId(int id);
        Task<IEnumerable<PerfilViewModel>> ObterTodos();
        Task<PaginacaoResultado<PerfilViewModel>> ObterTodosPaginados(int page, int pageSize);
        Task<Resultado> Cadastrar(PerfilViewModel modelo);
        Task<Resultado> Atualizar(PerfilViewModel modelo);
    }
}
