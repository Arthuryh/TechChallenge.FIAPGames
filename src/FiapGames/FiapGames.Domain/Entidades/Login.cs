namespace FiapGames.Domain.Entidades
{
    public class Login
    {
        public int IdLogin { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
