using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Aplicacao.Modelos.Produto;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Aplicacao.Modelos.Tamanho;
using PWMetricas.Aplicacao.Modelos.Origem;

namespace PWMetricas.Aplicacao.Servicos.Interfaces
{
    public interface IProdutoServico
    {
        Task<ProdutoViewModel> ObterPorId(int id);
        Task<PaginacaoResultado<ProdutoViewModel>> ObterTodosPaginados(int page, int pageSize);
        Task<Resultado> Cadastrar(ProdutoViewModel modelo);
        Task<Resultado> Atualizar(ProdutoViewModel modelo);
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();
    }
}
