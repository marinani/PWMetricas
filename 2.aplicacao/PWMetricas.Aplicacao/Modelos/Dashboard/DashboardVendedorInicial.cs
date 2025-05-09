using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos.Dashboard
{
    public class DashboardVendedorInicial
    {
        public Metas MinhasMetas { get; set; } = new Metas();

        public decimal SomaAtendimento { get; set; } = 0;
    }

    public class Metas
    {
        public decimal MetaMensalVendedor { get; set; } = 0;
        public decimal MetaMensal { get; set; } = 0;
        public decimal SuperMetaMensalVendedor { get; set; } = 0;
        public decimal SuperMetaMensal { get; set; } = 0;
        public decimal ValorMetaPorcentagemMetaMensal { get; set; } = 0;
        public decimal ValorMetaPorcentagemSuperMetaMensal { get; set; } = 0;
    }


    public class TarefasViewModel
    {
        public Guid Guid { get; set; }
        public string Cliente { get; set; }
        public string Data { get; set; }
        public string DataRetorno { get; set; }
        public string ValorPedido { get; set; }
        public string Status { get; set; }
        public string CorStatusAtendimento { get; set; }
    }

    public class  ResultadoViewModel
    {
        public decimal SomaAtendimento { get; set; }
        public decimal SomaOrcamento { get; set; }
        public decimal SomaVendido { get; set; }
        public decimal SomaNegociado { get; set; }
        public decimal SomaNaoResponde { get; set; }
    }
}
