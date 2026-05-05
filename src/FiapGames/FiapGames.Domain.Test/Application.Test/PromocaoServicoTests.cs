using FiapGames.Application.DTOs.Promocao;
using FiapGames.Application.Servicos;
using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces;
using Moq;

namespace FiapGames.Tests.Application
{
    public class PromocaoServicoTests
    {
        private readonly Mock<IPromocaoRepositorio> _repoMock;
        private readonly PromocaoServico _service;

        public PromocaoServicoTests()
        {
            _repoMock = new Mock<IPromocaoRepositorio>();
            _service = new PromocaoServico(_repoMock.Object);
        }

        [Fact]
        public async Task Deve_Criar_Promocao()
        {
            var dto = new CriarPromocaoDto("Promo", 10, DateTime.Now, DateTime.Now.AddDays(1));

            await _service.Criar(dto);

            _repoMock.Verify(x => x.Add(It.IsAny<Promocao>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Atualizar_Promocao_Inexistente()
        {
            _repoMock.Setup(x => x.GetById(It.IsAny<int>()))
                     .ReturnsAsync((Promocao)null);

            var dto = new AtualizarPromocaoDto(1, "Promo", 10, DateTime.Now, DateTime.Now.AddDays(1));

            await Assert.ThrowsAsync<ArgumentException>(() => _service.Atualizar(dto));
        }

        [Fact]
        public async Task Deve_Atualizar_Promocao()
        {
            var promo = new Promocao("Promo", 10, DateTime.Now, DateTime.Now.AddDays(1));

            _repoMock.Setup(x => x.GetById(It.IsAny<int>()))
                     .ReturnsAsync(promo);

            var dto = new AtualizarPromocaoDto(1, "Nova", 20, DateTime.Now, DateTime.Now.AddDays(2));

            await _service.Atualizar(dto);

            _repoMock.Verify(x => x.Update(promo), Times.Once);
        }
    }
}