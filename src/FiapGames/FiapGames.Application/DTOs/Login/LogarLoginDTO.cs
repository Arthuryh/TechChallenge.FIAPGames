using System.ComponentModel.DataAnnotations;

namespace FiapGames.Application.DTOs.Login
{
    public record LogarLoginDTO(
        [Required(ErrorMessage = "Credenciais inválidas")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Credenciais inválidas")]
        string Email,

        [Required(ErrorMessage = "Credenciais inválidas")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
            ErrorMessage = "Credenciais inválidas")]
        string PasswordHash
        );
}
