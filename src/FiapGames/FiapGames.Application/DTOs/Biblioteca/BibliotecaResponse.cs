using FiapGames.Application.DTOs.Jogo;

namespace FiapGames.Application.DTOs.Biblioteca
{
    public record BibliotecaResponse
    (
         int ContaId,
         IEnumerable<JogoResponseDto> Jogos
    );

}
