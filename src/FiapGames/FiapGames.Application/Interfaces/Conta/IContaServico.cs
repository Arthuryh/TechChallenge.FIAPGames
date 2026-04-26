using FiapGames.Application.DTOs.Conta;

namespace FiapGames.Application.Interfaces.Conta
{
    public interface IContaServico
    {
        Task<ObterSaldoDto> ObterSaldo(int idConta);
        Task<ObterSaldoDto> AdicionarSaldo(int id, AdicionarSaldoDto adicionarSaldo);
        Task<ObterSaldoDto> DebitarSaldo(int id, DebitarSaldoDto debitarSaldo);
    }
}
