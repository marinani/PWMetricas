﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Entidades
{
    public class Loja : EntidadeBase
    {
        public required string Nome { get; set; }
        public required string Cidade { get; set; }
    }
}
