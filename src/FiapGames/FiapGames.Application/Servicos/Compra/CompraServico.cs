using FiapGames.Application.DTOs.Compra;
using FiapGames.Application.Interfaces.Compra;
using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces.Compra;
using FiapGames.Infrastructure.Interfaces.Jogo;

namespace FiapGames.Application.Servicos.Compra
{
    public class CompraServico : ICompraServico
    {
        private readonly ICompraRepositorio _repo;
        private readonly IJogoRepositorio _jogoRepo;

        public CompraServico(ICompraRepositorio repo, IJogoRepositorio jogoRepo)
        {
            _repo = repo;
            _jogoRepo = jogoRepo;
        }

        public async Task CriarCompra(CriarCompraDto dto)
        {
            var compra = new Compra(0);

            foreach (var jogoId in dto.JogosIds)
            {
                var jogo = await _jogoRepo.GetById(jogoId);
                compra.AdicionarItem(jogo);
            }

            await _repo.Add(compra);
        }
    }
}
