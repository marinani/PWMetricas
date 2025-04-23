using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Tamanho;
using PWMetricas.Aplicacao.Modelos.Usuario;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface ITamanhoServico
    {
        Task<TamanhoViewModel> ObterPorId(int id);
        Task<PaginacaoResultado<TamanhoViewModel>> ObterTodosPaginados(int page, int pageSize);
        Task<Resultado> Cadastrar(TamanhoViewModel modelo);
        Task<Resultado> Atualizar(TamanhoViewModel modelo);
        Task<IEnumerable<TamanhoViewModel>> ObterTodos();
    }
}
