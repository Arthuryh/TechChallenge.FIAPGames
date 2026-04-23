namespace FiapGames.Application.DTOs.Login
{
    public class LerLoginDTO
    {
        public int IdLogin { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Ativo { get; set; }
    }
}
