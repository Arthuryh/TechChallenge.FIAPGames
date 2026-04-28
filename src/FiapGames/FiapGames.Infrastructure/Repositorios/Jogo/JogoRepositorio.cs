using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces.Jogo;
using Microsoft.EntityFrameworkCore;

namespace FiapGames.Infrastructure.Repositorios.Jogo
{
    public class JogoRepositorio : IJogoRepositorio
    {
        private readonly FIAPGamesContext _context;

        public JogoRepositorio(FIAPGamesContext context)
        {
            _context = context;
        }

        public async Task Add(Jogo jogo)
        {
            await _context.AddAsync(jogo);
            await _context.SaveChangesAsync();
        }

        public async Task<Jogo> GetById(int id)
        {
            return await _context.Jogos
                .Include(x => x.Promocao)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Jogo jogo)
        {
            _context.Update(jogo);
            await _context.SaveChangesAsync();
        }
    }
}
