namespace FiapGames.Application.Interfaces.Biblioteca
{
    public interface IBibliotecaServico
    {
        Task AdicionarJogo(int contaId, int jogoId);
        Task RemoverJogo(int contaId, int jogoId);
        Task AvaliarJogo(int contaId, int jogoId, int nota);
        Task<bool> PossuiJogo(int contaId, int jogoId);
    }
}