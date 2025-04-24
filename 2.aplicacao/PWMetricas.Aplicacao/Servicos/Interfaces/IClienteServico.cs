using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Aplicacao.Modelos.Cliente;
using PWMetricas.Aplicacao.Modelos;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IClienteServico
    {
        Task<ClienteViewModel> ObterPorGuid(Guid guid);
        Task<Resultado> Cadastrar(ClienteViewModel modelo);
        Task<PaginacaoResultado<ClienteViewModel>> ObterTodosPaginados(int page, int pageSize);
    }
}
