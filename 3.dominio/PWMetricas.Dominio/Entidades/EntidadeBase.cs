using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class EntidadeBase
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public bool Ativo { get; set; } = true;
    }
}
