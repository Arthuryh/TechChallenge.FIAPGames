namespace FiapGames.Application.DTOs.Promocao
{
    public record CriarPromocaoDto(
     string Nome,
     int TaxaDesconto,
     DateTime DataInicio,
     DateTime DataFim
 );
}
