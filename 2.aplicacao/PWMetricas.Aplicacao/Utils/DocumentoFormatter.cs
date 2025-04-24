using System.Text.RegularExpressions;

namespace PWMetricas.Aplicacao.Utils
{

    public static class DocumentoFormatter
    {
        /// <summary>
        /// Formata um documento como CPF ou CNPJ.
        /// </summary>
        /// <param name="documento">O número do documento (somente dígitos).</param>
        /// <returns>O documento formatado como CPF ou CNPJ, ou o valor original se inválido.</returns>
        public static string Formatar(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return documento;

            // Remove caracteres não numéricos
            documento = Regex.Replace(documento, @"\D", "");

            if (documento.Length == 11)
            {
                // Formata como CPF: 000.000.000-00
                return Regex.Replace(documento, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
            }
            else if (documento.Length == 14)
            {
                // Formata como CNPJ: 00.000.000/0000-00
                return Regex.Replace(documento, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");
            }

            // Retorna o valor original se não for CPF nem CNPJ
            return documento;
        }
    }
}
