using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces;

namespace FiapGames.Infrastructure.Repositorios
{
    public class CompraRepositorio : ICompraRepositorio
    {
        private readonly FIAPGamesContext _context;

        public CompraRepositorio(FIAPGamesContext context)
        {
            _context = context;
        }

        public async Task Add(Compra compra)
        {
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();
        }
    }
}
