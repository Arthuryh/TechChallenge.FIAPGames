using FiapGames.Application.DTOs.Compra;

namespace FiapGames.Application.Interfaces.Compra
{
    public interface ICompraServico
    {
            Task CriarCompra(CriarCompraDto dto);
    }
}
