using FiapGames.Application.DTOs.Compra;

namespace FiapGames.Application.Interfaces
{
    public interface ICompraServico
    {
            Task CriarCompra(CriarCompraDto dto);
    }
}
