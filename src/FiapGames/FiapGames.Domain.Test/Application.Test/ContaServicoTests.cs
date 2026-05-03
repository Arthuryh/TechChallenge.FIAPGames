using FiapGames.Application.Servicos;
using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces;
using Moq;

namespace FiapGames.Tests.Application
{
    public class ContaServicoTests
    {
        private readonly Mock<IContaRepositorio> _repoMock;
        private readonly ContaServico _service;

        public ContaServicoTests()
        {
            _repoMock = new Mock<IContaRepositorio>();
            _service = new ContaServico(_repoMock.Object);
        }

        [Fact]
        public async Task Deve_Obter_Saldo()
        {
            var conta = new Conta(100);

            _repoMock.Setup(x => x.ObterContaPorId(1)).ReturnsAsync(conta);

            var result = await _service.ObterSaldo(1);

            Assert.Equal(100, result.Valor);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Conta_Nao_Encontrada()
        {
            _repoMock.Setup(x => x.ObterContaPorId(It.IsAny<int>()))
                     .ReturnsAsync((Conta)null);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.ObterSaldo(1));
        }
    }
}