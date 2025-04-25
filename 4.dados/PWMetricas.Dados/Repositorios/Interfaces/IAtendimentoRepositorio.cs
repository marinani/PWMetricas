using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Dados.Repositorios.Interfaces
{
    public interface IAtendimentoRepositorio : IRepositorio<Atendimento>
    {
        Task<IEnumerable<Atendimento>> ObterEmAtendimentoPaginados(int page, int pageSize);
        Task<IEnumerable<Atendimento>> ObterEmOrcamentoPaginados(int page, int pageSize);
        Task<IEnumerable<Atendimento>> ObterVendidoPaginados(int page, int pageSize);
    }
}
