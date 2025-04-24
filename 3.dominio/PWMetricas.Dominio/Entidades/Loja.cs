using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class Loja : EntidadeBase
    {
        public required string NomeFantasia { get; set; }
        public required string Cep { get; set; }
        public required string Endereco { get; set; }
        public required string NumeroEndereco { get; set; }
        public required string Uf { get; set; }
        public required string Cidade { get; set; }
        public required string Bairro { get; set; }
        public string? Complemento { get; set; }
        public required string Telefone { get; set; }
        public required string CNPJ { get; set; }

        public string? ResponsavelEmpresa { get; set; }

        public required string Email { get; set; }
        public string? EmailCobranca { get; set; }
    }
}
