using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos.Perfil
{
    public class PerfilViewModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Título")]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }

    public class  PerfilSelect 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
