using FiapGames.Application.DTOs.Compra;
using FiapGames.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapGames.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ICompraServico _service;

        public CompraController(ICompraServico service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarCompraDto dto)
        {
            await _service.CriarCompra(dto);
            return Ok();
        }
    }
}
