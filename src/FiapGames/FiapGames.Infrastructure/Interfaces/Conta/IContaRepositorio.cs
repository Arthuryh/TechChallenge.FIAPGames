using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces
{
    public interface IContaRepositorio
    {
        Task<Conta?> ObterContaPorId(int id);
        Task<decimal> ObterSaldo(int idConta);
        Task AdicionarSaldo(Conta conta, decimal valor);
        Task DebitarSaldo(Conta conta, decimal valor);
    }
}
