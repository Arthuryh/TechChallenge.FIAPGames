using Bogus;
using FiapGames.Domain.Entidades;

namespace FiapGames.Tests.Domain
{
    public class JogoTests
    {
        private readonly Faker _faker = new Faker("pt_BR");

        [Fact]
        public void Deve_Criar_Jogo_Valido()
        {
            var nome = _faker.Commerce.ProductName();
            var descricao = _faker.Lorem.Sentence();
            var preco = _faker.Random.Decimal(10, 500);

            var jogo = new Jogo(nome, preco, descricao);

            Assert.Equal(nome, jogo.Nome);
            Assert.Equal(preco, jogo.Preco);
            Assert.Equal(descricao, jogo.Descricao);
            Assert.True(jogo.DataLancamento <= DateTime.Now);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Nome_For_Invalido()
        {
            var descricao = _faker.Lorem.Sentence();
            var preco = 100;

            Assert.Throws<ArgumentException>(() =>
                new Jogo("", preco, descricao));
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Descricao_For_Invalida()
        {
            var nome = _faker.Commerce.ProductName();
            var preco = 100;

            Assert.Throws<ArgumentException>(() =>
                new Jogo(nome, preco, ""));
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Preco_For_Invalido()
        {
            var nome = _faker.Commerce.ProductName();
            var descricao = _faker.Lorem.Sentence();

            Assert.Throws<ArgumentException>(() =>
                new Jogo(nome, 0, descricao));
        }

        [Fact]
        public void Deve_Retornar_Preco_Normal_Quando_Nao_Ha_Promocao()
        {
            var jogo = CriarJogoValido();

            var preco = jogo.ObterPrecoAtual();

            Assert.Equal(jogo.Preco, preco);
        }

        [Fact]
        public void Deve_Retornar_Preco_Com_Desconto_Quando_Promocao_Ativa()
        {
            var jogo = CriarJogoValido();

            var promocao = new Promocao(
                           _faker.Commerce.ProductName(),
                           taxa: 10,
                           inicio: DateTime.Now.AddDays(-1),
                           fim: DateTime.Now.AddDays(1));

            jogo.AplicarPromocao(promocao);

            var precoEsperado = jogo.Preco - (jogo.Preco * 0.10m);

            Assert.Equal(precoEsperado, jogo.ObterPrecoAtual());
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Promocao_Inativa()
        {
            var jogo = CriarJogoValido();

            var promocao = new Promocao(
               _faker.Commerce.ProductName(),
               taxa: 10,
               inicio: DateTime.Now.AddDays(-1),
               fim: DateTime.Now.AddDays(1));

            promocao.Desativar();

            Assert.Throws<Exception>(() =>
                jogo.AplicarPromocao(promocao));
        }

        [Fact]
        public void Deve_Aplicar_Promocao_Valida()
        {
            var IdPromocao = 1;
            var jogo = CriarJogoValido();

            var promocao = new Promocao(
               _faker.Commerce.ProductName(),
               taxa: 10,
               inicio: DateTime.Now.AddDays(-1),
               fim: DateTime.Now.AddDays(1));

            jogo.AplicarPromocao(promocao);

            Assert.NotNull(jogo.Promocao);
        }

        [Fact]
        public void Deve_Remover_Promocao()
        {
            var jogo = CriarJogoValido();

            var promocao = new Promocao(
                _faker.Commerce.ProductName(),
                taxa: 10,
                inicio: DateTime.Now.AddDays(-1),
                fim: DateTime.Now.AddDays(1)
            );

            jogo.AplicarPromocao(promocao);

            var precoEsperado = jogo.Preco - (jogo.Preco * 0.10m);

            Assert.Equal(precoEsperado, jogo.ObterPrecoAtual());
        }

        private Jogo CriarJogoValido()
        {
            return new Jogo(
                _faker.Commerce.ProductName(),
                _faker.Random.Decimal(50, 300),
                _faker.Lorem.Sentence()
            );
        }
    }
}