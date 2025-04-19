using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class Cliente : EntidadeBase
    {
        public required string Telefone { get; set; }
    }
}
