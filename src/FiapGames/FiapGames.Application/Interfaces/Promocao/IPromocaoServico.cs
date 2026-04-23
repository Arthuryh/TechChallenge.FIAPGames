namespace FiapGames.Application.Interfaces
{
    public interface IPromocaoServico
    {
        Task Criar(DTOs.Promocao.CriarPromocaoDto dto);
        Task Atualizar(DTOs.Promocao.AtualizarPromocaoDto dto);
        Task<List<DTOs.Promocao.PromocaoResponseDto>> ObterTodos();
        Task<DTOs.Promocao.PromocaoResponseDto> ObterPorId(int id);
        Task Deletar(int id);
    }
}
