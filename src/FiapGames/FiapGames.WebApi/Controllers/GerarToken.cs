using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FiapGames.WebApi.Controllers
{
    public class GerarTokenn
    {

        public string GerarToken(string email, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MINHA_CHAVE_SUPER_SECRETA_COM_32_BYTES!!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
