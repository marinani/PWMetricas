using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class Cidade : EntidadeBase
    {
        public required string Nome { get; set; }
        public required string Estado { get; set; }
    }
}
