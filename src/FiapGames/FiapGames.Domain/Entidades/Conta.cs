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
        public decimal Saldo { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public virtual Login Login { get; private set; }

        protected Conta() { }
        public Conta(decimal saldoInicial = 0)
        {
            Saldo = saldoInicial;
            DataAtualizacao = DateTime.UtcNow;
        }
        /*[Required]
        public int IdTipoConta { get; set; }*/
    }
}
