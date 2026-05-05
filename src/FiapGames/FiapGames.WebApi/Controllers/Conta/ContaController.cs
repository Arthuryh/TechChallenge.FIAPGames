using FiapGames.Application.DTOs.Conta;
using FiapGames.Application.Interfaces.Conta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapGames.WebApi.Controllers.Conta
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContaController : ControllerBase
    {
        private readonly IContaServico _contaServico;

        public ContaController(IContaServico contaServico)
        {
            _contaServico = contaServico;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterSaldo(int id)
        {
            var saldo = await _contaServico.ObterSaldo(id);
            return Ok(saldo);
        }

        [HttpPost("depositar")]
        public async Task<IActionResult> AdicionarSaldo(ContaDto dto)
        {
            await _contaServico.AdicionarSaldo(dto);
            return Ok();
        }
    }
}
