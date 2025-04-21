using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos.Usuario
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Nome")]
        public required string Nome { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "E-mail (acesso)")]
        public required string Email { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Senha")]
        public required string Senha { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Perfil")]
        public int? PerfilId { get; set; }

        public bool Ativo { get; set; }
    }
}
