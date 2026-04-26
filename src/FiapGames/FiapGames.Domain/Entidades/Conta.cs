using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiapGames.Domain.Entidades
{
    public class Conta
    {
        [Key]
        [Required]
        public int IdConta { get; set; }
        public int IdLogin { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Saldo { get; private set; }
        public DateTime DataAtualizacao { get; private set; }
        public virtual Login Login { get; private set; }

        protected Conta() { }
        public Conta(decimal saldoInicial = 0)
        {
            Saldo = saldoInicial;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void AdicionarSaldo(decimal valor)
        {
            if (valor <= 0) throw new ArgumentException("Valor a ser adicionado deve ser maior que zero.", nameof(valor));
            Saldo += valor;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void DebitarSaldo(decimal valor)
        {
            if (valor <= 0) throw new ArgumentException("Valor a ser debitado deve ser maior que zero.", nameof(valor));
            if (valor > Saldo) throw new InvalidOperationException("Saldo insuficiente para esta operação.");
            Saldo -= valor;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
