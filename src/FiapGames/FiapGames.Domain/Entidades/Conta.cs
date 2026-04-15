using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiapGames.Domain.Entidades
{
    public class Conta
    {
        [Key]
        [Required]
        public int IdConta { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Saldo { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public virtual Login Login { get; set; }
        public int IdLogin { get; set; }
        /*[Required]
        public int IdTipoConta { get; set; }*/
    }
}
