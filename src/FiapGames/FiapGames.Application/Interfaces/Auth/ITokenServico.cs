using FiapGames.Application.DTOs.Login;

namespace FiapGames.Application.Interfaces
{
    public interface ITokenServico
    {
        string GerarToken(LerLoginDTO login);
    }
}
