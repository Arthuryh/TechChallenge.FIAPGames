using FiapGames.Application.DTOs.Login;

namespace FiapGames.Application.Interfaces
{
    public interface ILoginServico
    {
        LoginDTO CriarLogin(LoginDTO loginDTO);
        LoginDTO ObterLoginPorEmail(string email);
    }
}
