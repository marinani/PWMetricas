using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Dominio.Filtros;

namespace PWMetricas.Dados.Repositorios.Interfaces
{
    public interface IAtendimentoRepositorio : IRepositorio<Atendimento>
    {
        Task<Atendimento> BuscarComObservacoes(Guid chave);
        Task<IEnumerable<Atendimento>> ObterAtendimentos(AtendimentoFiltro filtro);
        Task<IEnumerable<Atendimento>> ObterAtendimentosPaginados(int page, int pageSize, AtendimentoFiltro filtro);
        Task<int> ContarAtendimentos(AtendimentoFiltro filtro);
        Task<decimal?> SomaTotal(AtendimentoFiltro filtro);



    }
}
