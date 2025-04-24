using System.Text.RegularExpressions;

namespace PWMetricas.Dominio.Utils
{
    public class Geral
    {
        public static string ApenasNumeros(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return new string(str.Where(char.IsDigit).ToArray());
        }

        public static string PrimeiroNome(string nome)
        {
            var nomes = nome.Split(' ');

            var primeiroMome = nomes[0];

            return primeiroMome;
        }

        public static string RemoverAcentuacao(string texto)
        {
            var comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            var semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (var i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }

            return texto;
        }

        public static string RemoveCaracteresEspeciais(string texto)
        {
            return Regex.Replace(texto, @"[^0-9a-zA-Z\._]", " ");
        }
    }
}
