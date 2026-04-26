using FiapGames.Application.DTOs.Conta;
using FiapGames.Application.Interfaces.Conta;
using FiapGames.Infrastructure.Interfaces.Conta;

namespace FiapGames.Application.Servicos.Conta
{
    public class ContaServico : IContaServico
    {
        private readonly IContaRepositorio _repositorio;
        public ContaServico(IContaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ObterSaldoDto> ObterSaldo(int idConta)
        {
            var saldo = await _repositorio.ObterSaldo(idConta);

            if (saldo < 0) throw new Exception("Conta não encontrada.");

            return new ObterSaldoDto(saldo);
        }

        public async Task<ObterSaldoDto> AdicionarSaldo(int id, AdicionarSaldoDto adicionarSaldo)
        {
            await _repositorio.AdicionarSaldo(id, adicionarSaldo.Valor);

            return new ObterSaldoDto(adicionarSaldo.Valor);
        }

        public async Task<ObterSaldoDto> DebitarSaldo(int id, DebitarSaldoDto debitarSaldo)
        {
            var saldo = await _repositorio.ObterSaldo(id);
            if (saldo < debitarSaldo.Valor) throw new Exception("Saldo insuficiente.");
            await _repositorio.DebitarSaldo(id, debitarSaldo.Valor);
            
            return new ObterSaldoDto(debitarSaldo.Valor);
        }
    }
}
