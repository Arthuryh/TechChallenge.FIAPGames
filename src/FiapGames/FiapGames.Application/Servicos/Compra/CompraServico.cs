using FiapGames.Application.DTOs.Compra;
using FiapGames.Application.Interfaces.Compra;
using FiapGames.Application.Interfaces.Conta;
using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces;     

namespace FiapGames.Application.Servicos
{
    public class CompraServico : ICompraServico
    {
        private readonly ICompraRepositorio _repo;
        private readonly IJogoRepositorio _jogoRepo;
        private readonly IContaServico _contaServico;

        public CompraServico(ICompraRepositorio repo, IJogoRepositorio jogoRepo, IContaServico contaServico)
        {
            _repo = repo;
            _jogoRepo = jogoRepo;
            _contaServico = contaServico;
        }

        public async Task CriarCompra(CriarCompraDto dto)
        {
            var compra = new Compra(0);

            foreach (var jogoId in dto.JogosIds)
            {
                var jogo = await _jogoRepo.GetById(jogoId);
                if(jogo == null)
                    throw new ArgumentException("Jogo não encontrado: " + jogoId);
                compra.AdicionarItem(jogo);
            }

            await _repo.Add(compra);

            await _contaServico.DebitarSaldo(new DTOs.Conta.ContaDto(dto.IdUsuario, compra.ValorTotalLiquido));
        }
    }
}
