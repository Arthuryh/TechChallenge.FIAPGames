using FiapGames.Application.DTOs.Login;

namespace FiapGames.Application.Interfaces
{
    public interface ILoginServico
    {
        Task<CriarLoginDTO> CriarLogin(CriarLoginDTO loginDTO);
        Task<LerLoginDTO?> ObterLoginPorId(int id);
        Task<LerLoginDTO> ObterLoginPorEmail(string email);
        Task<IEnumerable<LerLoginDTO>> ObterLogins();
        Task AtualizarLogin(AtualizarLoginDTO loginDTO);
        Task DeletarLogin(int id);
        Task<LerLoginDTO>ValidarCredenciaisAsync(LogarLoginDTO loginDTO);
        Task TrocarSenhaAsync(TrocarSenhaDTO trocarSenha);
    }
}
