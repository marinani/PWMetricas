using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Aplicacao.Autenticacao.API
{
    public class Tokens
    {
        public Dictionary<string, Guid> Sistemas { get; set; } = new Dictionary<string, Guid>();
    }
}
