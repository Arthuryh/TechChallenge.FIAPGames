using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces.Promocao
{
    public interface IPromocaoRepositorio
    {
        Task<Promocao> GetById(int id);
        Task<List<Promocao>> GetAll();
        Task Add(Promocao promocao);
        Task Update(Promocao promocao);
    }
}
