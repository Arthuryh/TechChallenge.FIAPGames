namespace FiapGames.Application.DTOs.Jogo
{
    public record CriarJogoDto(
        string Nome,
        decimal Preco,
        string Descricao
    );
}
