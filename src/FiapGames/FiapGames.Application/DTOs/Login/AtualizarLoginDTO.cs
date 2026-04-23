namespace FiapGames.Application.DTOs.Login
{
    public record AtualizarLoginDTO(
        int IdLogin,
        string Nome,
        string Email,
        string PasswordHash
    );
}
