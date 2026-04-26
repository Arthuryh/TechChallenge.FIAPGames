using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces.Log;

namespace FiapGames.Infrastructure.Repositorios.Logs
{
    public class LogRepositorio : ILogRepositorio
    {
        private readonly FIAPGamesContext _context;

        public LogRepositorio(FIAPGamesContext context)
        {
            _context = context;
        }
        public async Task SalvarLogErro(LogError log)
        {
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
