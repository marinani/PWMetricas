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

        public int? Mes { get; set; } = DateTime.Now.Month;
        public int? Ano { get; set; } = DateTime.Now.Year;
        public string? NomeUsuario { get; set; }
        public List<TarefasViewModel> Tarefas { get; set; } = new List<TarefasViewModel>();
        public ResultadoViewModel Resultado { get; set; } = new ResultadoViewModel();
        public DashboardVendedorInicial Vendedor { get; set; } = new DashboardVendedorInicial();
        public DashboardGerenteViewModel Gerente { get; set; } = new DashboardGerenteViewModel();
    }

    public class DashboardGerenteViewModel
    {
      
        public decimal SomaAtendimento { get; set; } = 0;
        public List<MetasLojas> MetasLojas { get; set; } = new List<MetasLojas>();
        public List<ValorAtendimentoLoja> ValorAtendimentoLoja { get; set; } = new List<ValorAtendimentoLoja>();
    }

    public class MetasLojas
    {
        public string NomeLoja { get; set; }
        public decimal MetaMensalLoja { get; set; } = 0;
        public decimal MetaMensal { get; set; } = 0;
        public decimal SuperMetaMensalLoja { get; set; } = 0;
        public decimal SuperMetaMensal { get; set; } = 0;
        public decimal ValorMetaPorcentagemMetaMensal { get; set; } = 0;
        public decimal ValorMetaPorcentagemSuperMetaMensal { get; set; } = 0;
    }

    public class  ValorAtendimentoLoja
    {
        public string NomeLoja { get; set; }
        public decimal ValorEmAtendimento { get; set; }
        public decimal ValorOrcamento { get; set; }
        public decimal ValorVendido { get; set; }
        public decimal ValorPerdidoNegociado { get; set; }
        public decimal ValorPerdidoNaoResponde { get; set; }
    }
}
