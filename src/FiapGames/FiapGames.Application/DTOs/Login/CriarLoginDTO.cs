namespace FiapGames.Application.DTOs.Login
{
    public record CriarLoginDTO(
        string Nome,
        string Email,
        string PasswordHash
        );
}
