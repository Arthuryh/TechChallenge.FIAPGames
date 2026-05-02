using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces.Login
{
    public interface ILoginRepositorio
    {
        Task<Login?> ObterLoginPorId(int id);
        Task<Login?> ObterLoginPorEmail(string email);
        Task<IEnumerable<Login>> ObterLogins();
        Task AdicionarLogin(Login login);
        Task AtualizarLogin(Login login);
    }
}
