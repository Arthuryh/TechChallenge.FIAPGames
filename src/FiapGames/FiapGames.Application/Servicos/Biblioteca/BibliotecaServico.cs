using FiapGames.Application.DTOs.Biblioteca;
using FiapGames.Application.DTOs.Jogo;
using FiapGames.Application.DTOs.Promocao;
using FiapGames.Application.Interfaces.Biblioteca;
using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces;

public class BibliotecaServico : IBibliotecaServico
{
    private readonly IBibliotecaRepositorio _repo;
    private readonly IJogoRepositorio _jogoRepo;

    public BibliotecaServico(IBibliotecaRepositorio repo, IJogoRepositorio jogoRepo)
    {
        _repo = repo;
        _jogoRepo = jogoRepo;
    }

    public async Task AdicionarJogo(int contaId, int jogoId)
    {
        var biblioteca = await _repo.ObterPorConta(contaId);
        var jogo = await _jogoRepo.JogoPorId(jogoId);
        

        biblioteca.AdicionarJogo(jogo);

        await _repo.Atualizar(biblioteca);
    }

    public async Task RemoverJogo(int contaId, int jogoId)
    {
        var biblioteca = await _repo.ObterPorConta(contaId);

        biblioteca.RemoverJogo(jogoId);

        await _repo.Atualizar(biblioteca);
    }

    public async Task<bool> PossuiJogo(int contaId, int jogoId)
    {
        var biblioteca = await _repo.ObterPorConta(contaId);
        return biblioteca.Jogos.Any(x => x.JogoId == jogoId);
    }

    public async Task<BibliotecaResponse> BibliotecaUsuario(int contaId)
    {
        var biblioteca = await _repo.ObterPorConta(contaId);
        if (biblioteca == null)
            throw new ArgumentException("Biblioteca não encontrada para a conta informada.");

        var listaJogos = new List<Jogo>();

        foreach (var item in biblioteca.Jogos)
        {
            var jogo = await _jogoRepo.JogoPorId(item.JogoId);
            if (jogo == null)
                throw new ArgumentException("Jogo não encontrado");

            listaJogos.Add(jogo);
        }


        var bibliotecaResponse = new BibliotecaResponse
        (
            biblioteca.IdConta,
            listaJogos.Select(x => new BibliotecaJogoResponseDto
            (
                 x.Id,
                 x.Nome,
                 x.Preco,
                 x.ObterPrecoAtual(),
                 x.Descricao,
                 x.DataLancamento
            )).ToList()

        );

        return bibliotecaResponse;
    }
}