using FiapGames.Application.DTOs.Conta;

namespace FiapGames.Application.Interfaces.Conta
{
    public interface IContaServico
    {
        Task<ContaDto> ObterSaldo(int idConta);
        Task<ContaDto> AdicionarSaldo(ContaDto conta, decimal valor);
        Task<ContaDto> DebitarSaldo(ContaDto conta, decimal valor);
    }
}
