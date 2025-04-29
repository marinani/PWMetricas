using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos.Dashboard
{
    public class DashboardVendedorInicial
    {
        public string NomeUsuario { get; set; }
        public List<Tarefas> Tarefas { get; set; } = new List<Tarefas>();
        public Resultado Resultado { get; set; } = new Resultado();
    }

    public class Metas
    {
        public decimal? MetaMensalVendedor { get; set; }
        public decimal? MetaMensal { get; set; }
        public decimal? SuperMetaMensalVendedor { get; set; }
        public decimal? SuperMetaMensal { get; set; }
        public int? ValorMetaPorcentagemMetaMensal { get; set; }
        public int? ValorMetaPorcentagemSuperMetaMensal { get; set; }
    }


    public class Tarefas
    {
        public Guid Guid { get; set; }
        public string Cliente { get; set; }
        public string Data { get; set; }
        public string DataRetorno { get; set; }
        public string ValorPedido { get; set; }
        public string SomaTotal { get; set; }
    }

    public class  Resultados
    {
        public string SomaAtendimento { get; set; }
        public string SomaOrcamento { get; set; }
        public string SomaVendido { get; set; }
        public string SomaNegociado { get; set; }
        public string SomaNaoResponde { get; set; }
    }
}
