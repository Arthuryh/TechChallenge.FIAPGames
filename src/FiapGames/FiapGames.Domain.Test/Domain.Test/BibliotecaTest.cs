using Bogus;
using FiapGames.Domain.Entidades;

namespace FiapGames.Tests.Domain
{
    public class BibliotecaTests
    {
        private readonly Faker _faker = new Faker("pt_BR");

        [Fact]
        public void Deve_Criar_Biblioteca_Com_Conta()
        {
            var contaId = 1;

            var biblioteca = new Biblioteca(contaId);

            Assert.Equal(contaId, biblioteca.IdConta);
            Assert.NotNull(biblioteca.Jogos);
            Assert.Empty(biblioteca.Jogos);
        }

        [Fact]
        public void Deve_Adicionar_Jogo_Na_Biblioteca()
        {
            var biblioteca = CriarBiblioteca();
            var jogo = CriarJogo();

            biblioteca.AdicionarJogo(jogo);

            Assert.Single(biblioteca.Jogos);
            Assert.Contains(biblioteca.Jogos, x => x.JogoId == jogo.Id);
        }

        [Fact]
        public void Nao_Deve_Adicionar_Jogo_Duplicado()
        {
            var biblioteca = CriarBiblioteca();
            var jogo = CriarJogo();

            biblioteca.AdicionarJogo(jogo);
            ;

            Assert.Throws<ArgumentException>(() =>
              biblioteca.AdicionarJogo(jogo));
        }

        [Fact]
        public void Deve_Remover_Jogo_Da_Biblioteca()
        {
            var biblioteca = CriarBiblioteca();
            var jogo = CriarJogo();

            biblioteca.AdicionarJogo(jogo);
            biblioteca.RemoverJogo(jogo.Id);

            Assert.Empty(biblioteca.Jogos);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Ao_Remover_Jogo_Inexistente()
        {
            var biblioteca = CriarBiblioteca();

            Assert.Throws<ArgumentException>(() =>
                biblioteca.RemoverJogo(999));
        }

        private Biblioteca CriarBiblioteca()
        {
            return new Biblioteca(1);
        }

        private Jogo CriarJogo()
        {
            return new Jogo(
                _faker.Commerce.ProductName(),
                _faker.Random.Decimal(50, 300),
                _faker.Lorem.Sentence(),
                _faker.Random.Int(1, 1000)
            );
        }
    }
}