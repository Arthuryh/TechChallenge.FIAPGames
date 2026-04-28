using FiapGames.Application.DTOs.Jogo;
using FiapGames.Application.DTOs.Promocao;
using FiapGames.Application.Interfaces.Jogo;
using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces.Jogo;
using FiapGames.Infrastructure.Interfaces.Promocao;

namespace FiapGames.Application.Servicos.Jogo
{
    public class JogoServico : IJogoServico
    {
        private readonly IJogoRepositorio _repo;
        private readonly IPromocaoRepositorio _promoRepo;

        public JogoServico(IJogoRepositorio repo, IPromocaoRepositorio promoRepo)
        {
            _repo = repo;
            _promoRepo = promoRepo;
        }

        public async Task Criar(CriarJogoDto dto)
        {
            var jogo = new Jogo(dto.Nome, dto.Preco, dto.Descricao);
            await _repo.Add(jogo);
        }

        public async Task AplicarPromocao(AplicarPromocaoDto dto)
        {
            var jogo = await _repo.GetById(dto.JogoId);
            if (dto.PromocaoId == null || dto.PromocaoId == 0)
            {
                jogo.RemoverPromocao();
            }
            else
            {
                var promo = await _promoRepo.GetById(dto.PromocaoId);
                jogo.AplicarPromocao(promo);

            }


            await _repo.Update(jogo);
        }
    }
}
