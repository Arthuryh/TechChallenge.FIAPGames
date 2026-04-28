using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces.Promocao;
using Microsoft.EntityFrameworkCore;

namespace FiapGames.Infrastructure.Repositorios.Promocao
{
    public class PromocaoRepositorio : IPromocaoRepositorio
    {
        private readonly FIAPGamesContext _context;

        public PromocaoRepositorio(FIAPGamesContext context)
        {
            _context = context;
        }

        public async Task Add(Promocao promocao)
        {
            await _context.Promocoes.AddAsync(promocao);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Promocao>> GetAll()
        {
            return await _context.Promocoes.ToListAsync();
        }

        public async Task<Promocao> GetById(int id)
        {
            return await _context.Promocoes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Promocao promocao)
        {
            _context.Promocoes.Update(promocao);
            await _context.SaveChangesAsync();
        }
    }
}
