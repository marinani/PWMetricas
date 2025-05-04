using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PWMetricas.Aplicacao.Autenticacao.API;
using PWMetricas.Configuracao;

namespace PWMetricas.Api.Controllers
{
    /// <summary>
    /// Controller de autenticação, para poder utilizar outras Controllers da API.
    /// </summary>
    [AllowAnonymous]
    [Route("Api/[controller]")]
    public class AutenticacaoController : Controller
    {
        private readonly JwtIssuerOptions jwtOptions;
        private readonly JsonSerializerOptions serializerSettings;
        private readonly Tokens token;

        /// <summary>
        /// Criar uma instância da classe AutenticacaoController.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="tokensSnapshot"></param>
        public AutenticacaoController(IConfiguration configuration, IOptionsSnapshot<Tokens> tokensSnapshot)
        {
            // Configurar JwtIssuerOptions com base no appsettings
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            this.jwtOptions = new JwtIssuerOptions
            {
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
               SecurityAlgorithms.HmacSha256
               ),
                JtiGenerator = () => Task.FromResult(Guid.NewGuid().ToString()),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow
            };

            serializerSettings = new JsonSerializerOptions
            {
                WriteIndented = false
            };

            token = tokensSnapshot.Value;

            Console.WriteLine($"Chave de assinatura: {configuration["Jwt:Key"]}");
        }

        /// <summary>
        /// Autentica o usuário através dos dados de logon e fornece o token de autorização para as demais operações.
        /// </summary>
        /// <param name="guid">Código de acesso da aplicação.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<Resposta<Autorizacao>> Autenticacao(Guid guid)
        {
            var resposta = new Resposta<Autorizacao>();

            // Autenticação do cliente
            if (!token.Sistemas.ContainsValue(guid))
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                resposta.Erros.Add("Não autorizado.");
                return resposta;
            }

            // Permissões do cliente
            var sistema = string.Empty;
            var identity = GetClaimsIdentity(guid, out sistema);
            if (identity == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                resposta.Erros.Add("Funcionalidade não permitida.");
                return resposta;
            }

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, guid.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, await this.jwtOptions.JtiGenerator()),
        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtOptions.IssuedAt).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
    };

            var jwt = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                notBefore: jwtOptions.NotBefore,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: jwtOptions.SigningCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            resposta.Dados = new Autorizacao
            {
                TokenAcesso = encodedJwt,
                ValidoAte = jwt.ValidTo,
            };

            Console.WriteLine($"Token gerado: {encodedJwt}");

            return resposta;
        }

        #region Privados

        /// <summary>
        /// Converter uma data para Data em formato Unix, usado no jwt.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        /// <summary>
        /// Obter os direitos de acesso de uma determinada aplicação.
        /// </summary>
        /// <param name="guid">Código de acesso do sistema.</param>
        /// <param name="sistemaLicenca"></param>
        /// <returns></returns>
        private Task<ClaimsIdentity> GetClaimsIdentity(Guid guid, out string sistemaLicenca)
        {
            var sistema = token.Sistemas.FirstOrDefault(x => x.Value == guid);

            if (!sistema.Equals(default(KeyValuePair<string, Guid>)))
            {
                sistemaLicenca = sistema.Key;
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(guid.ToString(), "Token")));
            }

            sistemaLicenca = string.Empty;
            return Task.FromResult(new ClaimsIdentity());
        }

        #endregion Privados
    }
}
