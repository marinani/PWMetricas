using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Enumerators
{

    public class Mes : Enumerador
    {
        public static readonly Mes Janeiro = new(1, "Janeiro");
        public static readonly Mes Fevereiro = new(2, "Fevereiro");
        public static readonly Mes Marco = new(3, "Março");
        public static readonly Mes Abril = new(4, "Abril");
        public static readonly Mes Maio = new(5, "Maio");
        public static readonly Mes Junho = new(6, "Junho");
        public static readonly Mes Julho = new(7, "Julho");
        public static readonly Mes Agosto = new(8, "Agosto");
        public static readonly Mes Setembro = new(9, "Setembro");
        public static readonly Mes Outubro = new(10, "Outubro");
        public static readonly Mes Novembro = new(11, "Novembro");
        public static readonly Mes Dezembro = new(12, "Dezembro");

        public Mes()
        {
        }

        public Mes(int valor, string descricao) : base(valor, descricao)
        {
        }
    }
}
