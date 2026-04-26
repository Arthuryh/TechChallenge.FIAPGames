using System.ComponentModel.DataAnnotations;

namespace FiapGames.Domain.Entidades
{
    public class Login
    {
        [Key]
        [Required]
        public int IdLogin { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "O Nome não pode exceder 150 caracteres")]
        public string Nome { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "O E-mail não pode exceder 150 caracteres")]
        public string Email { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "A senha não pode exceder 255 caracteres")]
        public string PasswordHash { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        [Required]
        public TipoUsuario TipoUsuario { get; set; }
        public virtual Conta Conta { get; private set; }
        protected Login() { }
        public Login(string nome, string email, string passwordHash, int tipoUsuario)
        {
            Nome = nome;
            Email = email;
            PasswordHash = passwordHash;
            DataCriacao = DateTime.UtcNow;
            Ativo = true;
            TipoUsuario = (TipoUsuario)tipoUsuario;

            Conta = new Conta(0m);
        }

        public void AtualizarLogin(string nome, string email, string passwordHash)
        {
            Nome = nome;
            Email = email; 
            PasswordHash = passwordHash;
        }

        public void DesativarLogin()
        {
            Ativo = false;
        }
    }
}