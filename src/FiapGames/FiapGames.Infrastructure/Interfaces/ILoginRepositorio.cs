using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces
{
    public interface ILoginRepositorio
    {
        bool Criar(Login login);
    }
}
