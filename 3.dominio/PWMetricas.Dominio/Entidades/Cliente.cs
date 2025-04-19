using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class Cliente : EntidadeBase
    {
        public required string Nome { get; set; }
        public required string Telefone { get; set; }
    }
}
