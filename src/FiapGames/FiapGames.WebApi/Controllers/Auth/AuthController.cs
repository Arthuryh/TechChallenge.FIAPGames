using FiapGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FiapGames.WebApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenServico _tokenServico;
        private readonly ILoginServico _loginServico;

        public AuthController(ITokenServico tokenServico, ILoginServico loginServico)
        {
            _tokenServico = tokenServico;
            _loginServico = loginServico;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string senha)
        {
            var usuario = await _loginServico.ValidarCredenciaisAsync(email, senha);
            if (usuario == null)
            {
                return Unauthorized("Credenciais inválidas");
            }
            var token = _tokenServico.GerarToken(usuario);
            return Ok(new { Token = token });
        }
    }
}
