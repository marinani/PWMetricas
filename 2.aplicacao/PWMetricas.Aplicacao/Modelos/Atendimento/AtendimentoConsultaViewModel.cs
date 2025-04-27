using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Dominio.Filtros;

namespace PWMetricas.Aplicacao.Modelos.Atendimento
{
    public class AtendimentoConsultaViewModel
    {
        public AtendimentoFiltro Filtro { get; set; } = new AtendimentoFiltro();
        public IEnumerable<AtendimentoListaViewModel> Resultados { get; set; } = new List<AtendimentoListaViewModel>();

        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
    }
}
