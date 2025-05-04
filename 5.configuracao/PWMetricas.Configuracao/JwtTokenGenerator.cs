using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace PWMetricas.Configuracao
{
    public class JwtTokenGenerator
    {
        public static string GenerateToken()
        {
            var key = "+biO9KhfvK0J5ZhHMIzvwx0tLdpOaIK9zTi+v0K9ZtsKq+dUV1PKYlS4c5gJc8UMkRwgnWh+lzOdktwjDU+IfA=="; // Chave do appsettings
            var issuer = "http://api.pwmetricas.com.br/"; // Issuer do appsettings
            var audience = "dev.pwmetricas"; // Audience do appsettings

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, "Admin")
        };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24), // DuracaoHoras do appsettings
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
