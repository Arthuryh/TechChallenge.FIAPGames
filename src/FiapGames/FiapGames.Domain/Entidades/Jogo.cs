namespace FiapGames.Domain.Entidades
{
    public class Jogo
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataLancamento { get; private set; }

        public int? PromocaoId { get; private set; }
        public virtual Promocao? Promocao { get; private set; }

        public Jogo() { }

        public Jogo(string nome, decimal preco, string descricao)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            DataLancamento = DateTime.Now;
        }

        public decimal ObterPrecoAtual()
        {
            if (Promocao != null && Promocao.EstaAtiva())
                return Preco - (Preco * Promocao.TaxaDesconto / 100);

            return Preco;
        }

        public void AplicarPromocao(Promocao promocao)
        {
            if (!promocao.EstaAtiva())
                throw new Exception("Promoção inválida");

            Promocao = promocao;
            PromocaoId = promocao.Id;
        }

        public void RemoverPromocao()
        {
            Promocao = null;
        }
    }
}
