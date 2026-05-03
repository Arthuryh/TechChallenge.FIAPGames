using Moq;
using FiapGames.Application.Servicos;
using FiapGames.Infrastructure.Interfaces;
using FiapGames.Application.DTOs.Jogo;
using FiapGames.Application.DTOs.Promocao;
using FiapGames.Domain.Entidades;

namespace FiapGames.Tests.Application
{
    public class JogoServicoTests
    {
        private readonly Mock<IJogoRepositorio> _repoMock;
        private readonly Mock<IPromocaoRepositorio> _promoMock;
        private readonly JogoServico _service;

        public JogoServicoTests()
        {
            _repoMock = new Mock<IJogoRepositorio>();
            _promoMock = new Mock<IPromocaoRepositorio>();
            _service = new JogoServico(_repoMock.Object, _promoMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Jogo()
        {
            var dto = new CriarJogoDto("Jogo", 100, "Desc");

            await _service.Criar(dto);

            _repoMock.Verify(x => x.Add(It.IsAny<Jogo>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Aplicar_Promocao()
        {
            var jogo = new Jogo("Jogo", 100, "Desc");
            var promo = new Promocao("Promo", 10, DateTime.Now, DateTime.Now.AddDays(1));

            _repoMock.Setup(x => x.GetById(1)).ReturnsAsync(jogo);
            _promoMock.Setup(x => x.GetById(2)).ReturnsAsync(promo);

            var dto = new AplicarPromocaoDto(1, 2);

            await _service.AplicarPromocao(dto);

            _repoMock.Verify(x => x.Update(jogo), Times.Once);
        }
    }
}