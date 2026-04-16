using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces
{
    public interface ILoginRepositorio
    {
        Task AdicionarLogin(Login login);
        Task<Login?> ObterLoginPorId(int id);
        Task<IEnumerable<Login>> ObterLogins();
    }
}
