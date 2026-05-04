using FiapGames.Application.Servicos;
using FiapGames.Application.Servicos.Biblioteca;
using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces;
using Moq;

namespace FiapGames.Tests.Application
{
    public class BibliotecaServicoTests
    {
        private readonly Mock<IBibliotecaRepositorio> _repoMock;
        private readonly Mock<IJogoRepositorio> _jogoRepoMock;
        private readonly BibliotecaServico _service;

        public BibliotecaServicoTests()
        {
            _repoMock = new Mock<IBibliotecaRepositorio>();
            _jogoRepoMock = new Mock<IJogoRepositorio>();

            _service = new BibliotecaServico(
                _repoMock.Object,
                _jogoRepoMock.Object
            );
        }

        [Fact]
        public async Task Deve_Adicionar_Jogo_Na_Biblioteca()
        {
            var biblioteca = new Biblioteca(1);
            var jogo = CriarJogo();

            _repoMock.Setup(x => x.ObterPorConta(1))
                     .ReturnsAsync(biblioteca);

            _jogoRepoMock.Setup(x => x.GetById(2))
                         .ReturnsAsync(jogo);

            await _service.AdicionarJogo(1, 2);

            Assert.Single(biblioteca.Jogos);
            _repoMock.Verify(x => x.Atualizar(biblioteca), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Jogo_Nao_Encontrado()
        {
            var biblioteca = new Biblioteca(1);

            _repoMock.Setup(x => x.ObterPorConta(1))
                     .ReturnsAsync(biblioteca);

            _jogoRepoMock.Setup(x => x.GetById(It.IsAny<int>()))
                         .ReturnsAsync((Jogo)null);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.AdicionarJogo(1, 2));
        }

        [Fact]
        public async Task Deve_Remover_Jogo_Da_Biblioteca()
        {
            var biblioteca = new Biblioteca(1);
            var jogo = CriarJogo();

            biblioteca.AdicionarJogo(jogo);

            _repoMock.Setup(x => x.ObterPorConta(1))
                     .ReturnsAsync(biblioteca);

            await _service.RemoverJogo(1, jogo.Id);

            Assert.Empty(biblioteca.Jogos);
            _repoMock.Verify(x => x.Atualizar(biblioteca), Times.Once);
        }

        [Fact]
        public async Task Deve_Avaliar_Jogo()
        {
            var biblioteca = new Biblioteca(1);
            var jogo = CriarJogo();

            biblioteca.AdicionarJogo(jogo);

            _repoMock.Setup(x => x.ObterPorConta(1))
                     .ReturnsAsync(biblioteca);

            await _service.AvaliarJogo(1, jogo.Id, 5);

            Assert.Equal(5, biblioteca.Jogos.First().Avaliacao);
            _repoMock.Verify(x => x.Atualizar(biblioteca), Times.Once);
        }

        private Jogo CriarJogo()
        {
            return new Jogo("Jogo Teste", 100, "Desc");
        }
    }
}