using Microsoft.AspNetCore.Mvc;
using FiapGames.Application.Interfaces.Biblioteca;
using Microsoft.AspNetCore.Authorization;

namespace FiapGames.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BibliotecaController : ControllerBase
    {
        private readonly IBibliotecaServico _service;

        public BibliotecaController(IBibliotecaServico service)
        {
            _service = service;
        }

        [HttpDelete("{contaId}/jogos/{jogoId}")]
        public async Task<IActionResult> RemoverJogo(int contaId, int jogoId)
        {
            try
            {
                await _service.RemoverJogo(contaId, jogoId);
                return Ok(new { mensagem = "Jogo removido da biblioteca" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("{contaId}")]
        public async Task<IActionResult> BibliotecaUsuario(int contaId)
        {
            var biblioteca = await _service.BibliotecaUsuario(contaId);

            return Ok(biblioteca);
        }
    }
}