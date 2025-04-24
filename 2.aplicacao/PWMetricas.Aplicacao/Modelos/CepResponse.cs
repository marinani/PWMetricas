using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos
{
    public class CepResponse
    {
        public string Cep { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string Service { get; set; }
    }
}
