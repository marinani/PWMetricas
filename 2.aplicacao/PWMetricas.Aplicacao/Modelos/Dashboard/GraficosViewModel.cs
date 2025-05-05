using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos.Dashboard
{
    public class GraficosViewModel
    {
        public GraficoCoresDto OrigemAtendimento { get; set; }
        public GraficoCoresDto CanalAtendimento { get; set; }
        public GraficoSimplesDto VendedorAtendimento { get; set; }
        public GraficoSimplesDto CidadeAtendimento { get; set; }
    }

    public class GraficoCoresDto
    {
        public string Nome { get; set; }
        public decimal Porcentagem { get; set; } // agora traz percentuais, ex: 33.33
        public string CorHex { get; set; }
    }


    public class GraficoSimplesDto
    {
        public string Nome { get; set; }
        public decimal Porcentagem { get; set; } // agora traz percentuais, ex: 33.33
    }
}
