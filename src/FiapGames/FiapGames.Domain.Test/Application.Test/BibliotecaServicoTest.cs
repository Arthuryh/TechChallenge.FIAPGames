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

            _jogoRepoMock.Setup(x => x.JogoPorId(2))
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

            _jogoRepoMock.Setup(x => x.JogoPorId(It.IsAny<int>()))
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
        public async Task Deve_Retornar_True_Quando_Possui_Jogo()
        {
            var biblioteca = new Biblioteca(1);
            var jogo = CriarJogo();

            biblioteca.AdicionarJogo(jogo);

            _repoMock.Setup(x => x.ObterPorConta(1))
                     .ReturnsAsync(biblioteca);

            var result = await _service.PossuiJogo(1, jogo.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task Deve_Retornar_False_Quando_Nao_Possui_Jogo()
        {
            var biblioteca = new Biblioteca(1);

            _repoMock.Setup(x => x.ObterPorConta(1))
                     .ReturnsAsync(biblioteca);

            var result = await _service.PossuiJogo(1, 999);

            Assert.False(result);
        }

        [Fact]
        public async Task Deve_Retornar_Biblioteca_Com_Jogos()
        {
            var biblioteca = new Biblioteca(1);
            var jogo = CriarJogo();

            biblioteca.AdicionarJogo(jogo);

            _repoMock.Setup(x => x.ObterPorConta(1))
                     .ReturnsAsync(biblioteca);

            _jogoRepoMock.Setup(x => x.JogoPorId(jogo.Id))
                         .ReturnsAsync(jogo);

            var result = await _service.BibliotecaUsuario(1);

            Assert.NotNull(result);
            Assert.Single(result.Jogos);
            Assert.Equal(jogo.Id, result.Jogos.First().Id);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Biblioteca_Nao_Encontrada()
        {
            _repoMock.Setup(x => x.ObterPorConta(It.IsAny<int>()))
                     .ReturnsAsync((Biblioteca)null);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.BibliotecaUsuario(1));
        }

        [Fact]
        public async Task Deve_Mapear_Promocao_Quando_Existir()
        {
            var biblioteca = new Biblioteca(1);

            var promocao = new Promocao("Promo", 10, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            var jogo = CriarJogo();

            jogo.AplicarPromocao(promocao);
            biblioteca.AdicionarJogo(jogo);

            _repoMock.Setup(x => x.ObterPorConta(1))
                     .ReturnsAsync(biblioteca);

            _jogoRepoMock.Setup(x => x.JogoPorId(jogo.Id))
                         .ReturnsAsync(jogo);

            var result = await _service.BibliotecaUsuario(1);

            Assert.NotNull(result.Jogos.First().Promocao);
        }

        [Fact]
        public async Task Deve_Retornar_Promocao_Nula_Quando_Nao_Existir()
        {
            var biblioteca = new Biblioteca(1);
            var jogo = CriarJogo();

            biblioteca.AdicionarJogo(jogo);

            _repoMock.Setup(x => x.ObterPorConta(1))
                     .ReturnsAsync(biblioteca);

            _jogoRepoMock.Setup(x => x.JogoPorId(jogo.Id))
                         .ReturnsAsync(jogo);

            var result = await _service.BibliotecaUsuario(1);

            Assert.Null(result.Jogos.First().Promocao);
        }

        private Jogo CriarJogo()
        {
            return new Jogo("Jogo Teste", 100, "Desc",1);
        }
    }
}