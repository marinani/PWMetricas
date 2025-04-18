using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PWMetricas.Dominio.Utils
{
    public static class CriptoUtil
    {
        public static string Encrypt(string input)
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }

        public static bool Compare(string input, string hash)
        {
            var inputHash = Encrypt(input);
            return inputHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
