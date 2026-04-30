using FiapGames.Application.DTOs.Conta;

namespace FiapGames.Application.Interfaces.Conta
{
    public interface IContaServico
    {
        Task<ContaDto> ObterSaldo(int idConta);
        Task<ContaDto> AdicionarSaldo(ContaDto conta);
        Task<ContaDto> DebitarSaldo(ContaDto conta);
    }
}
