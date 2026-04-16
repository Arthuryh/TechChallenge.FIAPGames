using FiapGames.Application.DTOs;

namespace FiapGames.Application.Interfaces
{
    public interface ILoginServico
    {
        Task<CriarLoginDTO> CriarLogin(CriarLoginDTO loginDTO);
        Task<LerLoginDTO?> ObterLoginPorId(int id);
        Task<IEnumerable<LerLoginDTO>> ObterLogins();
    }
}
