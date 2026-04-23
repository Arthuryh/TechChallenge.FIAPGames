using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces
{
    public interface IJogoRepositorio
    {
        Task<Jogo> GetById(int id);
        Task Add(Jogo jogo);
        Task Update(Jogo jogo);
    }
}
