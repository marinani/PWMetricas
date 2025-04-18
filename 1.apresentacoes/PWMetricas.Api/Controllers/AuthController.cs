using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace PWMetricas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] GuidRequest request)
        {
            // Obtém o GUID do appsettings
            var configuredGuid = _configuration["Jwt:Guid"];

            // Verifica se o GUID fornecido corresponde ao configurado
            if (request.Guid != configuredGuid)
            {
                return Unauthorized(new { message = "GUID inválido." });
            }

            // Gera o token JWT
            var token = GenerateJwtToken();
            return Ok(new { token });
        }

        private string GenerateJwtToken()
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "SwaggerUser"),
                new Claim(ClaimTypes.Role, "Admin") // Role fixa para o token
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class GuidRequest
    {
        public string Guid { get; set; }
    }
}
