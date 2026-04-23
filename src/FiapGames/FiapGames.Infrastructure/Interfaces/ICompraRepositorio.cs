using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces
{
    public interface ICompraRepositorio
    {
        Task Add(Compra compra);
    }
}
