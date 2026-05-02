using Bogus;
using FiapGames.Domain.Entidades;

namespace FiapGames.UnitTests.Domain
{
    public class PromocaoTests
    {
        private readonly Faker _faker = new Faker("pt_BR");

        [Fact]
        public void Deve_Criar_Promocao_Valida()
        {
            var nome = _faker.Commerce.ProductName();
            var taxa = 10;
            var inicio = DateTime.Now.AddDays(-1);
            var fim = DateTime.Now.AddDays(1);

            var promocao = new Promocao(nome, taxa, inicio, fim);

            Assert.Equal(nome, promocao.Nome);
            Assert.Equal(taxa, promocao.TaxaDesconto);
            Assert.True(promocao.Ativo);
        }

        [Fact]
        public void Deve_Estar_Ativa_Quando_Dentro_Do_Periodo()
        {
            var promocao = CriarPromocaoValida();

            var ativa = promocao.EstaAtiva();

            Assert.True(ativa);
        }

        [Fact]
        public void Deve_Nao_Estar_Ativa_Quando_Fora_Do_Periodo()
        {
            var promocao = new Promocao(
                _faker.Commerce.ProductName(),
                10,
                DateTime.Now.AddDays(-10),
                DateTime.Now.AddDays(-5)
            );

            var ativa = promocao.EstaAtiva();

            Assert.False(ativa);
        }

        [Fact]
        public void Deve_Nao_Estar_Ativa_Quando_Desativada()
        {
            var promocao = CriarPromocaoValida();

            promocao.Desativar();

            Assert.False(promocao.EstaAtiva());
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Taxa_Invalida()
        {
            var nome = _faker.Commerce.ProductName();

            Assert.Throws<ArgumentException>(() =>
                new Promocao(nome, 0, DateTime.Now, DateTime.Now.AddDays(1)));
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Data_Final_Menor_Que_Inicial()
        {
            var nome = _faker.Commerce.ProductName();

            Assert.Throws<ArgumentException>(() =>
                new Promocao(
                    nome,
                    10,
                    DateTime.Now,
                    DateTime.Now.AddDays(-1)
                ));
        }

        [Fact]
        public void Deve_Aplicar_Desconto_Corretamente()
        {
            var promocao = CriarPromocaoValida();
            var preco = 100m;

            var precoComDesconto = preco - preco * (promocao.TaxaDesconto / 100m);

            Assert.Equal(90m, precoComDesconto);
        }

        private Promocao CriarPromocaoValida()
        {
            return new Promocao(
                _faker.Commerce.ProductName(),
                10,
                DateTime.Now.AddDays(-1),
                DateTime.Now.AddDays(1)
            );
        }
    }
}