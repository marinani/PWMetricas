using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        // Navegação
        public virtual Perfil Perfil { get; set; }
        public int PerfilId { get; set; }
    }
}
