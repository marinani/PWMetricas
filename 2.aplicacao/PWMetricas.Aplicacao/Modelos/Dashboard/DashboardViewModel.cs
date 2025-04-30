using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos.Dashboard
{
    public class DashboardViewModel
    {
        public int? LojaId { get; set; }
        public DashboardVendedorInicial Vendedor { get; set; } = new DashboardVendedorInicial();
    }
}
