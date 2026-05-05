using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapGames.Infrastructure.Repositorios.Biblioteca
{
    public class BibliotecaRepositorio : IBibliotecaRepositorio
    {
        private readonly FIAPGamesContext _context;

        public BibliotecaRepositorio(FIAPGamesContext context)
        {
            _context = context;
        }

        public async Task Adicionar(Domain.Entidades.Biblioteca biblioteca)
        {
            await _context.Bibliotecas.AddAsync(biblioteca);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Domain.Entidades.Biblioteca biblioteca)
        {
            _context.Bibliotecas.Update(biblioteca);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Entidades.Biblioteca> ObterPorConta(int contaId)
        {
            var biblioteca = await _context.Bibliotecas
                .Include(b => b.Jogos)
                .FirstOrDefaultAsync(b => b.IdConta == contaId);

            if (biblioteca == null)
            {
                biblioteca = new Domain.Entidades.Biblioteca(contaId);

                await _context.Bibliotecas.AddAsync(biblioteca);
                await _context.SaveChangesAsync();
            }

            return biblioteca;
        }
    }
}