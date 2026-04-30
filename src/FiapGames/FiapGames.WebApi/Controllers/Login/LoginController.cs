using FiapGames.Application.DTOs.Login;
using FiapGames.Application.Interfaces.Login;
using Microsoft.AspNetCore.Mvc;

namespace FiapGames.WebApi.Controllers.Login
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
            return Ok(novoLogin);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var usuario = await _loginServico.ObterLoginPorId(id);
            return Ok(usuario);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> ObterPorEmail(string email)
        {
            var usuario = await _loginServico.ObterLoginPorEmail(email);
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosLogins()
        {
            var usuarios = await _loginServico.ObterLogins();
            return Ok(usuarios);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarLogin([FromBody] AtualizarLoginDTO loginDTO)
        {
            await _loginServico.AtualizarLogin(loginDTO);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarLogin(int id)
        {
            await _loginServico.DeletarLogin(id);
            return Ok();
        }
    }
}