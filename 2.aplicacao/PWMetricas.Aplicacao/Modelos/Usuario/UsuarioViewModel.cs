using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Aplicacao.Modelos.Perfil;

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

        public string PerfilNome{ get; set; }

        public bool Ativo { get; set; }
    }


    public class UsuarioConsulta
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

        public PerfilSelect Perfil { get; set; } = new PerfilSelect();

        public bool Ativo { get; set; }
    }


}
