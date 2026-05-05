using System.ComponentModel.DataAnnotations;

namespace FiapGames.Application.DTOs.Promocao
{
    public record AplicarPromocaoDto(
        [Required(ErrorMessage = "O ID de um jogo é obrigatório para aplicar a promoção.")]
        int JogoId,

        [Required(ErrorMessage = "O ID de uma promoção é obrigatório para aplicar a promoção.")]
        int PromocaoId
    );
}
