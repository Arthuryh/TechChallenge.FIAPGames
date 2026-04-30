using FiapGames.Application.DTOs.Promocao;
using FiapGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapGames.WebApi.Controllers.Promocao
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PromocaoController : ControllerBase
    {
        private readonly IPromocaoServico _service;

        public PromocaoController(IPromocaoServico service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarPromocaoDto dto)
        {
            await _service.Criar(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarPromocaoDto dto)
        {
            await _service.Atualizar(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.ObterTodos();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.ObterPorId(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Deletar(id);
            return Ok();
        }
    }
}
