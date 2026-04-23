using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces
{
    public interface ILoginRepositorio
    {
        Task AdicionarLogin(Login login);
        Task<Login?> ObterLoginPorId(int id);
        Task<Login?> ObterLoginPorEmail(string email);
        Task<IEnumerable<Login>> ObterLogins();
        Task AtualizarLogin(Login login);
        Task DesativarLogin(int id);
    }
}
