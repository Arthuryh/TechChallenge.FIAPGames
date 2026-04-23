using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapGames.Infrastructure.Repositorios
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly FIAPGamesContext _context;

        public LoginRepositorio(FIAPGamesContext context)
        {
            _context = context;
        }

        public async Task AdicionarLogin(Login login)
        {
            await _context.Logins.AddAsync(login);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarLogin(Login login)
        {
            _context.Logins.Update(login);
            await _context.SaveChangesAsync();
        }

        public async Task DesativarLogin(int id)
        {
            await _context.Logins
                .Where(l => l.IdLogin == id)
                .ExecuteUpdateAsync(l => l
                .SetProperty(p => p.Ativo, false));
        }

        public async Task<Login?> ObterLoginPorEmail(string email)
        {
            return await _context.Logins
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Email == email);
        }

        public async Task<Login?> ObterLoginPorId(int id)
        {
            return await _context.Logins
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.IdLogin == id);
        }

        public async Task<IEnumerable<Login>> ObterLogins()
        {
            return await _context.Logins
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
