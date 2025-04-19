using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class Loja : EntidadeBase
    {
        public string Nome { get; set; }
        public string Cidade { get; set; }
    }
}
