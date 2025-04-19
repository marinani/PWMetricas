using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class UsuarioLoja : EntidadeBase
    {
        public required int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public required int LojaId { get; set; }
        public Loja Loja { get; set; }
    }
}
