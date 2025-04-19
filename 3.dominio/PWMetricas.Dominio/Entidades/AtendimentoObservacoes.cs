using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class AtendimentoObservacoes : EntidadeBase
    {
        public DateTime Data { get; set; }
        public required string Descricao { get; set; }
        public int AtendimentoId { get; set; }
        public virtual Atendimento Atendimento { get; set; }
    }
}
