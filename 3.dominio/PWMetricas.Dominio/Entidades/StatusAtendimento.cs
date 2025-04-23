using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class StatusAtendimento : EntidadeBase
    {
        public required string Nome { get; set; }
        public string? CorHex { get; set; } // Ex: "#25D366"

    }
    
}
