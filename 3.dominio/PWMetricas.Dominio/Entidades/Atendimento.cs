using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class Atendimento : EntidadeBase
    {
        public required string Nome { get; set; }
        public required decimal Valor { get; set; }
        public required DateTime Data { get; set; }
        public required DateTime? DataRetorno { get; set; }
        public int CanalId { get; set; }
        public virtual Canal Canal { get; set; }
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public int CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }
        public decimal ValorPedido { get; set; }

        //Vendedor
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
