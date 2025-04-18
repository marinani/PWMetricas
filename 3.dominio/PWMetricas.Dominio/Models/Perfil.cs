using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
