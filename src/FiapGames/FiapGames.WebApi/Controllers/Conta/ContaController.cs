using FiapGames.Application.DTOs.Conta;
using FiapGames.Application.Interfaces.Conta;
using Microsoft.AspNetCore.Mvc;

namespace FiapGames.WebApi.Controllers.Conta
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaServico _contaServico;

        public ContaController(IContaServico contaServico)
        {
            _contaServico = contaServico;
        }

        [HttpGet("{id:int}/saldo")]
        public async Task<IActionResult> ObterSaldo(int id)
        {
            var saldo = await _contaServico.ObterSaldo(id);
            return Ok(saldo);
        }

        [HttpPost("{id:int}/depositar")]
        public async Task<IActionResult> AdicionarSaldo(int id, [FromBody] AdicionarSaldoDto dto)
        {
            await _contaServico.AdicionarSaldo(id, dto);
            return Ok();
        }

        [HttpPost("{id:int}/debitar")]
        public async Task<IActionResult> DebitarSaldo(int id, [FromBody] DebitarSaldoDto dto)
        {
            await _contaServico.DebitarSaldo(id, dto);
            return Ok();
        }
    }
}
