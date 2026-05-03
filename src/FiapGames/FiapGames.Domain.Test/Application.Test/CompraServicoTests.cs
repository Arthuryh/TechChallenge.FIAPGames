using Moq;
using FiapGames.Application.Servicos;
using FiapGames.Infrastructure.Interfaces;
using FiapGames.Application.DTOs.Compra;
using FiapGames.Domain.Entidades;

namespace FiapGames.Tests.Application
{
    public class CompraServicoTests
    {
        private readonly Mock<ICompraRepositorio> _repoMock;
        private readonly Mock<IJogoRepositorio> _jogoMock;
        private readonly CompraServico _service;

        public CompraServicoTests()
        {
            _repoMock = new Mock<ICompraRepositorio>();
            _jogoMock = new Mock<IJogoRepositorio>();
            _service = new CompraServico(_repoMock.Object, _jogoMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Compra_Com_Jogos()
        {
            var jogo = new Jogo("Jogo", 100, "Desc");

            _jogoMock.Setup(x => x.GetById(It.IsAny<int>()))
                     .ReturnsAsync(jogo);

            var dto = new CriarCompraDto(new List<int> { 1, 2 },1);

            await _service.CriarCompra(dto);

            _repoMock.Verify(x => x.Add(It.IsAny<Compra>()), Times.Once);
        }
    }
}