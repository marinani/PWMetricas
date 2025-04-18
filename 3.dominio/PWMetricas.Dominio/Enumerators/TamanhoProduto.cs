using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Enumerators
{
    public enum TamanhoProduto
    {
        [Display(Name = "Colchão")]
        Colchao = 1,
        [Display(Name = "Colchão + Base")]
        ColchaoBase = 2,
        [Display(Name = "Colchão + Base Box")]
        ColchaoBaseBox = 3,
        [Display(Name = "Topper Visco")]
        TopperVisco = 4
    }
}
