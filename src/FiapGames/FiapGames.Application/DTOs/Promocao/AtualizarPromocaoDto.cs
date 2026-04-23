namespace FiapGames.Application.DTOs.Promocao
{
    public record AtualizarPromocaoDto(
        int Id,
        string Nome,
        int TaxaDesconto,
        DateTime DataInicio,
        DateTime DataFim
     );

}
