using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Enumerators
{
    public enum OrigemVenda
    {
        [Display(Name = "Presencial")]
        Presencial = 1,
        [Display(Name = "Google")]
        Google = 2,
        [Display(Name = "Prospecção")]
        Prospeccao = 3,
        [Display(Name = "Instagram")]
        Instagram = 4,
        [Display(Name = "Já é cliente")]
        Cliente = 5,
    }
}
