using FiapGames.Application.DTOs.Compra;
using FiapGames.Application.Interfaces.Compra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapGames.WebApi.Controllers.Compra
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
