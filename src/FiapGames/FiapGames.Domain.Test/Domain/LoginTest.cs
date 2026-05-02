using Bogus;
using FiapGames.Domain.Entidades;

namespace FiapGames.UnitTests.Domain
{
    public class LoginTests
    {
        private readonly Faker _faker = new Faker("pt_BR");

        [Fact]
        public void Deve_Criar_Login_Valido()
        {
            var login = CriarLogin();

            Assert.NotNull(login);
            Assert.True(login.Ativo);
            Assert.NotNull(login.Conta);
            Assert.Equal(0, login.Conta.Saldo);
            Assert.True(login.DataCriacao <= DateTime.UtcNow);
        }

        [Fact]
        public void Deve_Atualizar_Dados_Do_Login()
        {
            var login = CriarLogin();

            var novoNome = _faker.Name.FullName();
            var novoEmail = _faker.Internet.Email();
            var novaSenha = _faker.Internet.Password();

            login.AtualizarLogin(novoNome, novoEmail, novaSenha);

            Assert.Equal(novoNome, login.Nome);
            Assert.Equal(novoEmail, login.Email);
            Assert.Equal(novaSenha, login.PasswordHash);
        }

        [Fact]
        public void Deve_Desativar_Login()
        {
            var login = CriarLogin();

            login.DesativarLogin();

            Assert.False(login.Ativo);
        }

        private Login CriarLogin()
        {
            return new Login(
                _faker.Name.FullName(),
                _faker.Internet.Email(),
                _faker.Internet.Password(),
                1
            );
        }
    }
}