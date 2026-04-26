namespace FiapGames.Application.DTOs.Login
{
    public record LerLoginDTO(
        int IdLogin,
        string Nome,
        string Email,
        string PasswordHash,
        string Ativo,
        int TipoUsuario
        );
}
