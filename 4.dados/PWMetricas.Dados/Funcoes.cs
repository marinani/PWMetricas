using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PWMetricas.Dados
{
    public static class Funcoes
    {
        public static string FnRemoverAcento(string? valor)
        {
            if (string.IsNullOrEmpty(valor))
                return string.Empty;

            // Etapa 1: Normalizar para decompor os caracteres acentuados
            string textoNormalizado = valor.Normalize(NormalizationForm.FormD);

            // Etapa 2: Remover marcas de acento (diacríticos)
            string textoSemAcento = Regex.Replace(textoNormalizado, @"\p{Mn}", "");

            // Etapa 3: Substituir caracteres especiais manualmente
            textoSemAcento = textoSemAcento
                .Replace('ç', 'c')
                .Replace('Ç', 'C')
                .Replace('ñ', 'n')
                .Replace('Ñ', 'N');

            // Etapa 4: Normalizar novamente se necessário
            return textoSemAcento.Normalize(NormalizationForm.FormC);
        }
    }
}
