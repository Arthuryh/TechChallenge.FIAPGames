using FiapGames.Domain.Entidades;

namespace FiapGames.UnitTests
{
    public class ContaTests
    {
        [Fact]
        public void Deve_Criar_Conta_Com_Saldo_Inicial()
        {
            var conta = new Conta(100);

            Assert.Equal(100, conta.Saldo);
            Assert.True(conta.DataAtualizacao <= DateTime.UtcNow);
        }

        [Fact]
        public void Deve_Adicionar_Valor_Ao_Saldo()
        {
            var conta = new Conta(100);

            conta.Adicionar(50);

            Assert.Equal(150, conta.Saldo);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Adicionar_Valor_Invalido()
        {
            var conta = new Conta();

            Assert.Throws<ArgumentException>(() => conta.Adicionar(0));
        }

        [Fact]
        public void Deve_Debitar_Valor_Do_Saldo()
        {
            var conta = new Conta(100);

            conta.Debitar(40);

            Assert.Equal(60, conta.Saldo);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Saldo_Insuficiente()
        {
            var conta = new Conta(50);

            Assert.Throws<ArgumentException>(() => conta.Debitar(100));
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Debitar_Valor_Invalido()
        {
            var conta = new Conta(50);

            Assert.Throws<ArgumentException>(() => conta.Debitar(0));
        }
    }
}