using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastExpressionCompiler;
using Mapster;
using PWMetricas.Dominio.Enumerators;

namespace PWMetricas.Configuracao
{
    public static class Mapeamento
    {
        public static void Registrar()
        {
            {
                // melhora performance do mapster;
                TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileFast();
                //TypeAdapterConfig.GlobalSettings.Default.MaxDepth(5);

                TypeAdapterConfig<Enumerador, int?>.NewConfig().Map(d => d, x => x.Valor, enumerador => enumerador != null);

                TypeAdapterConfig<Enumerador, string>
                    .NewConfig()
                    .Map(d => d, x => x.Descricao, enumerador => enumerador != null);


            }
        }
    }
}
