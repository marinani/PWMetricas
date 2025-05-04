using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace PWMetricas.Configuracao
{
    public class JwtIssuerOptions
    {
        /// <summary>
        /// "iss" (Issuer) Claim
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// "aud" (Audience) Claim
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// "nbf" (Not Before) Claim (default is UTC NOW)
        /// </summary>
        public DateTime NotBefore { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// "iat" (Issued At) Claim (default is UTC NOW)
        /// </summary>
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Define o tempo de validade do token (configurável via appsettings)
        /// </summary>
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromHours(24);

        /// <summary>
        /// "exp" (Expiration Time) Claim (returns IssuedAt + ValidFor)
        /// </summary>
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        /// <summary>
        /// "jti" (JWT ID) Claim (default ID is a GUID)
        /// </summary>
        public Func<Task<string>> JtiGenerator { get; set; } = () => Task.FromResult(Guid.NewGuid().ToString());

        /// <summary>
        /// A chave de assinatura usada para gerar tokens.
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
