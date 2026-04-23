namespace FiapGames.Application.DTOs.Jogo
{
    public record JogoResponseDto(
     int Id,
     string Nome,
     decimal Preco,
     decimal PrecoAtual,
     string Descricao,
     DateTime DataLancamento,
     int? PromocaoId
 );
}
