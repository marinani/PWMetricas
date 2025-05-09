﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos.Loja
{
    public class LojaViewModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Nome Fantasia")]
        public string? NomeFantasia { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "Whatsapp")]
        public string? Telefone { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "CNPJ")]
        public string? CNPJ { get; set; }
        [Display(Name = "Responsavel pela Empresa")]
        public string? ResponsavelEmpresa { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Dominio.Mensagens.Mensagens))]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de e-mail válido.")]
        public string? Email { get; set; }
        [Display(Name = "E-mail da Cobrança")]
        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de e-mail válido.")]
        public string? EmailCobranca { get; set; }
        [Display(Name = "Cep")]
        public string? Cep { get; set; }
        [Display(Name = "Logradouro")]
        public string? Endereco { get; set; }
        [Display(Name = "Nº")]
        public string? NumeroEndereco { get; set; }
        [Display(Name = "UF")]
        public string? Uf { get; set; }
        [Display(Name = "Cidade")]
        public string? Cidade { get; set; }
        [Display(Name = "Bairro")]
        public string? Bairro { get; set; }
        [Display(Name = "Complemento")]
        public string? Complemento { get; set; }
    }

    public class LojaSimplesViewModel
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }
    }
}
