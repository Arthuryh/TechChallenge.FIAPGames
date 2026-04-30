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