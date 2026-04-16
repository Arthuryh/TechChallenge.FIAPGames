using FiapGames.Application.DTOs;
using FiapGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FiapGames.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServico _loginServico;

        public LoginController(ILoginServico loginServico)
        {
            _loginServico = loginServico;
        }

        [HttpPost]
        public async Task<IActionResult> CriarLogin([FromBody] CriarLoginDTO loginDTO)
        {
            CriarLoginDTO novoLogin = await _loginServico.CriarLogin(loginDTO);
            return CreatedAtAction(nameof(ObterPorId), novoLogin);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var usuario = await _loginServico.ObterLoginPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosLogins()
        {
            var usuarios = await _loginServico.ObterLogins();
            return Ok(usuarios);
        }
    }
}