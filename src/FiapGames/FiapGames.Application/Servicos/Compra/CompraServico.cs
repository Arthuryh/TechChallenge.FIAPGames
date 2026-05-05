using FiapGames.Application.DTOs.Compra;
using FiapGames.Application.Interfaces.Biblioteca;
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
        private readonly IBibliotecaServico _bibliotecaServico;

        public CompraServico(ICompraRepositorio repo, IJogoRepositorio jogoRepo, IContaServico contaServico, IBibliotecaServico bibliotecaServico)
        {
            _repo = repo;
            _jogoRepo = jogoRepo;
            _contaServico = contaServico;
            _bibliotecaServico = bibliotecaServico;
        }

        public async Task CriarCompra(CriarCompraDto dto)
        {
            var compra = new Compra(0);

            foreach (var jogoId in dto.JogosIds)
            {
                var jogo = await _jogoRepo.JogoPorId(jogoId);
                if (jogo == null)
                    throw new ArgumentException("Jogo não encontrado: " + jogoId);
                compra.AdicionarItem(jogo);
            }

            await _repo.Add(compra);

            await _contaServico.DebitarSaldo(new DTOs.Conta.ContaDto(dto.IdUsuario, compra.ValorTotalLiquido));

            foreach (var jogoId in dto.JogosIds)
            {
                await _bibliotecaServico.AdicionarJogo(dto.IdUsuario, jogoId);
            }
        }
    }
}
