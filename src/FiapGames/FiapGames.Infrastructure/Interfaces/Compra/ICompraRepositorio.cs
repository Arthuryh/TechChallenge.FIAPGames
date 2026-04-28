using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces.Compra
{
    public interface ICompraRepositorio
    {
        Task Add(Compra compra);
    }
}
