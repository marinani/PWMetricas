using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Modelos.Atendimento
{
    public class AtendimentoListaViewModel
    {
         public int Id { get; set; }
         public Guid Guid { get; set; }
      
        public string Data { get; set; } 
        public string? Whatsapp { get; set; }
        public string? DataRetorno { get; set; }
        public string? Uf { get; set; }
        public string? Cidade { get; set; }
        public string ValorPedido { get; set; }
        public string Status { get; set; }
        public string Cliente { get; set; }
    }
}
