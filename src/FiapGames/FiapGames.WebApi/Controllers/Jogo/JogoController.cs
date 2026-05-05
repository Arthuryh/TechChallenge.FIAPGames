using FiapGames.Application.DTOs.Jogo;
using FiapGames.Application.DTOs.Promocao;
using FiapGames.Application.Interfaces.Jogo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapGames.WebApi.Controllers.Jogo
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JogoController : ControllerBase
    {
        private readonly IJogoServico _service;

        public JogoController(IJogoServico service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Criar(CriarJogoDto dto)
        {
            await _service.Criar(dto);
            return Ok();
        }

        [HttpPost("promocao")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AplicarPromo(AplicarPromocaoDto dto)
        {
            await _service.AplicarPromocao(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Jogos()
        {
            var jogos = await _service.ListaJogos();
            return Ok(jogos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> JogoPorId(int id)
        {
            var jogo = await _service.JogoPorId(id);
            return Ok(jogo);
        }
    }
}
