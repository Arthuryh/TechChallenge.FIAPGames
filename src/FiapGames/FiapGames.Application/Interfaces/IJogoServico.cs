using FiapGames.Application.DTOs.Jogo;
using FiapGames.Application.DTOs.Promocao;

namespace FiapGames.Application.Interfaces
{
    public interface IJogoServico
    {
        Task Criar(CriarJogoDto dto);
        Task AplicarPromocao(AplicarPromocaoDto dto);
    }
}
