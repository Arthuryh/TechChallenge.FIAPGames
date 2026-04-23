namespace FiapGames.Application.DTOs.Promocao
{
    public record PromocaoResponseDto(
        int Id,
        string Nome,
        int TaxaDesconto,
        DateTime DataInicio,
        DateTime DataFim,
        bool Ativo
    );
}
