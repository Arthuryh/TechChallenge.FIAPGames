using FiapGames.Application.DTOs.Biblioteca;

namespace FiapGames.Application.Interfaces.Biblioteca
{
    public interface IBibliotecaServico
    {
        Task AdicionarJogo(int contaId, int jogoId);
        Task RemoverJogo(int contaId, int jogoId);
        Task<bool> PossuiJogo(int contaId, int jogoId);
        Task<BibliotecaResponse>BibliotecaUsuario(int contaId);
    }
}