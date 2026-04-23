namespace FiapGames.Application.DTOs.Compra
{
    public record CriarCompraDto(
        List<int> JogosIds,
        int IdUsuario
    );
}
