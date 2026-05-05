using FiapGames.Application.DTOs.Jogo;
using FiapGames.Application.DTOs.Promocao;

namespace FiapGames.Application.Interfaces.Jogo
{
    public interface IJogoServico
    {
        Task Criar(CriarJogoDto dto);
        Task AplicarPromocao(AplicarPromocaoDto dto);
        Task<IEnumerable<JogoResponseDto>> ListaJogos();
        Task<JogoResponseDto> JogoPorId(int idJogo);
    }
}
