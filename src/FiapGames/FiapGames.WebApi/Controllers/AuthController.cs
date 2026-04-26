using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapGames.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        GerarTokenn GerarToken = new GerarTokenn();
        public AuthController()
        {

        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult User()
        {
            return Ok("Qualquer usuário logado");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return Ok("Só admin");
        }

        [HttpGet("Solo")]
        public IActionResult Solo()
        {
            return Ok("Sem auth");
        }

        [HttpGet("Auth")]
        public IActionResult GetTokenUser([FromQuery] string email, [FromQuery] string tipo)
        {
            var token = GerarToken.GerarToken(email, tipo);
            return Ok(token);
        }
    }
}

