namespace FiapGames.Application.DTOs.Jogo
{
    public record AtualizarJogoDto(
        int Id,
        string Nome,
        decimal Preco,
        string Descricao
    );
}
