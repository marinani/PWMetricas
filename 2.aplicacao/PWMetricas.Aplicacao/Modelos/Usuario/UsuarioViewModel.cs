using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWMetricas.Aplicacao.Modelos.Loja;
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
        [Display(Name = "Confirmação E-mail")]
        public string ConfirmaEmail { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Confirmação Senha")]
        public string ConfirmaSenha { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Perfil")]
        public int? PerfilId { get; set; }

        public int? LojaId { get; set; }

        public string PerfilNome { get; set; }

        public bool Ativo { get; set; }

       
        [Display(Name = "Meta Mensal")]
        public decimal? MetaMensal { get; set; }

       
        [Display(Name = "Super-Meta Mensal")]
        public decimal? SuperMetaMensal { get; set; }
    }

    public class VendedorViewModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Nome Completo")]
        public required string Nome { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "E-mail (acesso)")]
        public required string Email { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Senha")]
        public required string Senha { get; set; }
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Confirmação E-mail")]
        public string? ConfirmaEmail { get; set; }
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Confirmação Senha")]
        public string? ConfirmaSenha { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Filial")]
        public int? LojaId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Meta Mensal")]
        public string? MetaMensal { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Super-Meta Mensal")]
        public string? SuperMetaMensal { get; set; }
    }


    public class UsuarioConsulta
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "E-mail (acesso)")]
        public string Email { get; set; }
       
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Perfil")]
        public int? PerfilId { get; set; }
        public int? LojaId { get; set; }

        public PerfilSelect Perfil { get; set; } = new PerfilSelect();

        public string LojaNome { get; set; }

        public bool Ativo { get; set; }

        public decimal? MetaMensal { get; set; }

        public decimal? SuperMetaMensal { get; set; }
    }

    public class  UsuarioSenhaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Senha Atual")]
        public string Senha { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Nova Senha")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Confirmação Senha")]
        public string ConfirmaSenha { get; set; }
    }

    public class UsuarioSelect
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }


}
