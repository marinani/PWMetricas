using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Enumerators
{
    public enum Canal
    {
        [Display(Name = "Loja - Presencial")]
        Loja = 1,
        [Display(Name = "Whatsapp - Online")]
        Whatsapp = 2,
    }
}
