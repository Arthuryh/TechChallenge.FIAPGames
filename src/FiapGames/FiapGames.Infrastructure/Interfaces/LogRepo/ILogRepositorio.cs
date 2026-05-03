using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces.LogRepo
{
    public interface ILogRepositorio
    {
        Task SalvarLogErro(LogMensagem log);
    }
}
