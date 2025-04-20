using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class EntidadeBase
    {
        public EntidadeBase() { }

        public EntidadeBase(int id)
        {
            Id = id;
        }

        public EntidadeBase(int id, Guid guid)
        {
            Id = id;
            Guid = guid;
        }

        public required int Id { get; set; }
        public required Guid Guid { get; set; } = Guid.NewGuid();
        public required bool Ativo { get; set; } = true;
    }


}
