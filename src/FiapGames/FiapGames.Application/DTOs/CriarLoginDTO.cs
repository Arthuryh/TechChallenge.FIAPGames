using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FiapGames.Application.DTOs
{
    public class CriarLoginDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        [PasswordPropertyText(true)]
        public string PasswordHash { get; set; }
    }
}
