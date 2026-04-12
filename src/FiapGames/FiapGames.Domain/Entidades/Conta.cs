namespace FiapGames.Domain.Entidades
{
    public class Conta
    {
        public int IdConta { get; set; }
        public decimal Saldo { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int IdTipoConta { get; set; }
    }
}
