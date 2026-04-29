using FiapGames.Application.DTOs.Conta;
using FiapGames.Application.Interfaces.Conta;
using FiapGames.Infrastructure.Interfaces;

namespace FiapGames.Application.Servicos
{
    public class ContaServico : IContaServico
    {
        private readonly IContaRepositorio _repositorio;
        public ContaServico(IContaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ContaDto> ObterSaldo(int idConta)
        {
            var conta = await _repositorio.ObterContaPorId(idConta);

            if (conta != null) return new ContaDto(conta.IdConta, conta.Saldo);

            throw new ArgumentException("Conta não encontrada.");
        }

        public async Task<ContaDto> AdicionarSaldo(ContaDto contaDto, decimal valor)
        {
            var conta = await _repositorio.ObterContaPorId(contaDto.IdConta);

            if (conta == null) throw new ArgumentException("Conta não encontrada.");
            
            conta.Adicionar(valor);
            await _repositorio.AdicionarSaldo(conta, valor);
            return new ContaDto(conta.IdConta, conta.Saldo);
        }

        public async Task<ContaDto> DebitarSaldo(ContaDto contaDto, decimal valor)
        {
            var conta = await _repositorio.ObterContaPorId(contaDto.IdConta);
            if (conta == null) throw new ArgumentException("Conta não encontrada.");
            
            conta.Debitar(valor);
            await _repositorio.DebitarSaldo(conta, valor);
            return new ContaDto(conta.IdConta, conta.Saldo);
        }
    }
}
