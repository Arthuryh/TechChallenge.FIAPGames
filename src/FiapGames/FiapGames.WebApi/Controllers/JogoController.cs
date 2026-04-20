using FiapGames.Application.DTOs.Jogo;
using FiapGames.Application.DTOs.Promocao;
using FiapGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapGames.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly IJogoServico _service;

        public JogoController(IJogoServico service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarJogoDto dto)
        {
            await _service.Criar(dto);
            return Ok();
        }

        [HttpPost("promocao")]
        public async Task<IActionResult> AplicarPromo(AplicarPromocaoDto dto)
        {
            await _service.AplicarPromocao(dto);
            return Ok();
        }
    }
}
