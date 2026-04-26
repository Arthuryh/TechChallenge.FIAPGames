using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces.Log
{
    public interface ILogRepositorio
    {
        Task SalvarLogErro(LogError log);
    }
}
