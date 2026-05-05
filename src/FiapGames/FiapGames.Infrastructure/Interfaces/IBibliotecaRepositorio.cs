using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Interfaces
{
    public interface IBibliotecaRepositorio
    {
        Task<Biblioteca> ObterPorConta(int contaId);
        Task Adicionar(Biblioteca biblioteca);
        Task Atualizar(Biblioteca biblioteca);
    }
}
