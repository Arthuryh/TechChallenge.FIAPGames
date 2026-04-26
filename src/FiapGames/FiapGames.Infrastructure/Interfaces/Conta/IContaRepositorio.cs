namespace FiapGames.Infrastructure.Interfaces.Conta
{
    public interface IContaRepositorio
    {
        Task<decimal> ObterSaldo(int idConta);
        Task AdicionarSaldo(int idConta, decimal valor);
        Task DebitarSaldo(int idConta, decimal valor);
    }
}
