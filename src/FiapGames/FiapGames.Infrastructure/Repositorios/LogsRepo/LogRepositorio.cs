using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces.LogRepo;

namespace FiapGames.Infrastructure.Repositorios.LogsRepo
{
    public class LogRepositorio : ILogRepositorio
    {
        private readonly FIAPGamesContext _context;

        public LogRepositorio(FIAPGamesContext context)
        {
            _context = context;
        }
        public async Task SalvarLogErro(LogMensagem log)
        {
            try
            {

                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
