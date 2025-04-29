using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos
{
    public class PaginacaoResultado<T>
    {
        public IEnumerable<T> Dados { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
        public string? SomaTotal { get; set; }
    }
}
