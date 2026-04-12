using FiapGames.Application.DTOs;

namespace FiapGames.Application.Interfaces
{
    public interface ILoginServico
    {
        LoginDTO CriarLogin(LoginDTO loginDTO);
        LoginDTO ObterLoginPorEmail(string email);
    }
}
