using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class Atendimento : EntidadeBase
    {
        public required DateTime Data { get; set; }
        public DateTime? DataRetorno { get; set; }
        public int CanalId { get; set; }
        public virtual Canal Canal { get; set; }
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public decimal ValorPedido { get; set; }

        //Vendedor
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int TamanhoId { get; set; }
        public virtual Tamanho Tamanho { get; set; }

        public int OrigemId { get; set; }
        public virtual Origem Origem { get; set; }

        public int StatusAtendimentoId { get; set; }

        public virtual StatusAtendimento StatusAtendimento { get; set; }

        public required string Whatsapp { get; set; }

        public string? Uf { get; set; }
        public string? Cidade { get; set; }

        public int LojaId { get; set; }

        public virtual Loja Loja { get; set; }

        public virtual List<AtendimentoObservacoes> AtendimentoObservacoes { get; set; }
    }
}
