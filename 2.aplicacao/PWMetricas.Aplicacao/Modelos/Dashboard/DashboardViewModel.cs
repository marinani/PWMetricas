using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Dominio.Filtros;

namespace PWMetricas.Aplicacao.Modelos.Dashboard
{
    public class DashboardViewModel
    {
        //public AtendimentoConsultaViewModel AtendimentoConsulta { get; set; } = new AtendimentoConsultaViewModel();
        public AtendimentoFiltro Filtro { get; set; } = new AtendimentoFiltro();
        public IEnumerable<AtendimentoListaViewModel> Resultados { get; set; } = new List<AtendimentoListaViewModel>();

        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
        public bool IsVendedor { get; set; } = false;
        public int? LojaId { get; set; }
        public string ValorPedido { get; set; } = "0,00";
        public int? Mes { get; set; } = DateTime.Now.Month;
        public int? Ano { get; set; } = DateTime.Now.Year;
        public string? NomeUsuario { get; set; }
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
