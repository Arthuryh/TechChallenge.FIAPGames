using Moq;
using FiapGames.Application.Servicos;
using FiapGames.Infrastructure.Interfaces;
using FiapGames.Application.DTOs.Login;
using FiapGames.Domain.Entidades;

namespace FiapGames.Tests.Application
{
    public class LoginServicoTests
    {
        private readonly Mock<ILoginRepositorio> _repoMock;
        private readonly LoginServico _service;

        public LoginServicoTests()
        {
            _repoMock = new Mock<ILoginRepositorio>();
            _service = new LoginServico(_repoMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Login()
        {
            var dto = new CriarLoginDTO("Nome", "email@test.com", "123", 1);

            await _service.CriarLogin(dto);

            _repoMock.Verify(x => x.AdicionarLogin(It.IsAny<Login>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Login_Nao_Encontrado()
        {
            _repoMock.Setup(x => x.ObterLoginPorId(It.IsAny<int>()))
                     .ReturnsAsync((Login)null);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.ObterLoginPorId(1));
        }
    }
}