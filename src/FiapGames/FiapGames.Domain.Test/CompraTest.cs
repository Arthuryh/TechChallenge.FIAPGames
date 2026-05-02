using Bogus;
using FiapGames.Domain.Entidades;

namespace FiapGames.UnitTests
{
    public class CompraTests
    {
        private readonly Faker _faker = new Faker("pt_BR");

        [Fact]
        public void Deve_Criar_Compra_Com_Data_Atual()
        {
            var compra = new Compra(1);

            Assert.Equal(1, compra.Id);
            Assert.True(compra.DataCompra <= DateTime.Now);
        }

        [Fact]
        public void Deve_Adicionar_Item_E_Atualizar_Valores()
        {
            var compra = new Compra(1);
            var jogo = CriarJogoValido();

            compra.AdicionarItem(jogo);

            Assert.NotNull(compra.CompraJogos);
            Assert.Single(compra.CompraJogos);

            Assert.Equal(jogo.Preco, compra.ValorTotalBruto);
            Assert.Equal(jogo.ObterPrecoAtual(), compra.ValorTotalLiquido);
        }

        [Fact]
        public void Deve_Acumulaar_Valores_Ao_Adicionar_Multiplos_Itens()
        {
            var compra = new Compra(1);
            var jogo1 = CriarJogoValido();
            var jogo2 = CriarJogoValido();

            compra.AdicionarItem(jogo1);
            compra.AdicionarItem(jogo2);

            Assert.Equal(jogo1.Preco + jogo2.Preco, compra.ValorTotalBruto);
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