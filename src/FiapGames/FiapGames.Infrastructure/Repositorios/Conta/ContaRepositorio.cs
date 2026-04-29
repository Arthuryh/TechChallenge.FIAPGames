using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using FiapGames.Domain.Entidades;

namespace FiapGames.Infrastructure.Repositorios
{
    public class ContaRepositorio : IContaRepositorio
    {
        private readonly FIAPGamesContext _context;

        public ContaRepositorio(FIAPGamesContext context)
        {
            _context = context;
        }

        public async Task AdicionarSaldo(Conta conta, decimal valor)
        {
            await _context.Contas
                .Where(c => c.IdConta == conta.IdConta)
                .ExecuteUpdateAsync(c => c
                .SetProperty(p => p.Saldo, p => p.Saldo + valor)
                .SetProperty(p => p.DataAtualizacao, DateTime.UtcNow));
        }

        public async Task DebitarSaldo(Conta conta, decimal valor)
        {
            await _context.Contas
                .Where(c => c.IdConta == conta.IdConta)
                .ExecuteUpdateAsync(c => c
                .SetProperty(p => p.Saldo, p => p.Saldo - valor)
                .SetProperty(p => p.DataAtualizacao, DateTime.UtcNow));
        }

        public async Task<Conta?> ObterContaPorId(int id)
        {
             return await _context.Contas
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdConta == id);

        }

        public async Task<decimal> ObterSaldo(int idConta)
        {
            return await _context.Contas
                .AsNoTracking()
                .Where(c => c.IdConta == idConta)
                .Select(c => c.Saldo)
                .FirstOrDefaultAsync();
        }
    }
}
