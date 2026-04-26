using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces.Conta;
using Microsoft.EntityFrameworkCore;

namespace FiapGames.Infrastructure.Repositorios.Conta
{
    public class ContaRepositorio : IContaRepositorio
    {
        private readonly FIAPGamesContext _context;

        public ContaRepositorio(FIAPGamesContext context)
        {
            _context = context;
        }

        public async Task AdicionarSaldo(int idConta, decimal valor)
        {
            await _context.Contas
                .Where(c => c.IdConta == idConta)
                .ExecuteUpdateAsync(c => c
                .SetProperty(p => p.Saldo, p => p.Saldo + valor)
                .SetProperty(p => p.DataAtualizacao, DateTime.UtcNow));
        }

        public async Task DebitarSaldo(int idConta, decimal valor)
        {
            await _context.Contas
                .Where(c => c.IdConta == idConta)
                .ExecuteUpdateAsync(c => c
                .SetProperty(p => p.Saldo, p => p.Saldo - valor)
                .SetProperty(p => p.DataAtualizacao, DateTime.UtcNow));
        }

        public async Task<decimal> ObterSaldo(int idConta)
        {
            var conta = await _context.Contas
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdConta == idConta);
            if (conta is null)
                throw new Exception("Conta não encontrada.");
            return conta.Saldo;
        }
    }
}
