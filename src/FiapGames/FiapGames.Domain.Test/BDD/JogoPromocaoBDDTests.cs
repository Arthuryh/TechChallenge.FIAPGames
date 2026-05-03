using Bogus;
using FiapGames.Domain.Entidades;

namespace FiapGames.Tests.BDD
{
    public class JogoPromocaoBDDTests
    {
        private readonly Faker _faker = new Faker("pt_BR");

        [Fact]
        public void Dado_Um_Jogo_Com_Promocao_Ativa_Quando_Obter_Preco_Entao_Deve_Aplicar_Desconto()
        {
            // DADO
            var jogo = CriarJogoValido();

            var promocao = new Promocao(
                "Promo 10%",
                10,
                DateTime.Now.AddDays(-1),
                DateTime.Now.AddDays(1)
            );

            jogo.AplicarPromocao(promocao);

            // QUANDO
            var precoAtual = jogo.ObterPrecoAtual();

            // ENTÃO
            var esperado = jogo.Preco * 0.9m;

            Assert.Equal(esperado, precoAtual);
        }

        [Fact]
        public void Dado_Um_Jogo_Sem_Promocao_Quando_Obter_Preco_Entao_Deve_Retornar_Preco_Normal()
        {
            // DADO
            var jogo = CriarJogoValido();

            // QUANDO
            var precoAtual = jogo.ObterPrecoAtual();

            // ENTÃO
            Assert.Equal(jogo.Preco, precoAtual);
        }

        [Fact]
        public void Dado_Uma_Promocao_Expirada_Quando_Aplicar_Entao_Deve_Lancar_Excecao()
        {
            // DADO
            var jogo = CriarJogoValido();

            var promocao = new Promocao(
                "Promo Expirada",
                10,
                DateTime.Now.AddDays(-10),
                DateTime.Now.AddDays(-5)
            );

            // QUANDO / ENTÃO
            Assert.Throws<Exception>(() =>
                jogo.AplicarPromocao(promocao));
        }

        [Fact]
        public void Dado_Uma_Promocao_Desativada_Quando_Aplicar_Entao_Nao_Deve_Permitir()
        {
            // DADO
            var jogo = CriarJogoValido();

            var promocao = new Promocao(
                "Promo",
                10,
                DateTime.Now.AddDays(-1),
                DateTime.Now.AddDays(1)
            );

            promocao.Desativar();

            // QUANDO / ENTÃO
            Assert.Throws<Exception>(() =>
                jogo.AplicarPromocao(promocao));
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