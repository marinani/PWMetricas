using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Filtros
{
    public class AtendimentoFiltro
    {
        public int? UsuarioId { get; set; }
        public int? LojaId { get; set; }
        public int? StatusAtendimentoId { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
