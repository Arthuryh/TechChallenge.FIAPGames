using FiapGames.Application.DTOs;
using FiapGames.Application.Interfaces;
using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces;

namespace FiapGames.Application.Servicos
{
    public class LoginServico : ILoginServico
    {
        private readonly ILoginRepositorio _repositorio;
        public LoginServico(ILoginRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<CriarLoginDTO> CriarLogin(CriarLoginDTO loginDTO)
        {
            // 1. DE PARA (Mapeamento): O serviço recebe o DTO e transforma na Entidade
            var novoLogin = new Login(loginDTO.Nome, loginDTO.Email, loginDTO.PasswordHash);

            // 2. Chama o Repositório para salvar a Entidade no banco
            await _repositorio.AdicionarLogin(novoLogin);

            // 3. Retorna o DTO de volta para quem chamou (o Controller da WebApi)
            return loginDTO;
        }
        public async Task<LerLoginDTO?> ObterLoginPorId(int id)
        {
            var login = await _repositorio.ObterLoginPorId(id);

            if (login == null) return null;

            // Mapear a Entidade de volta para o DTO
            var retornoDTO = new LerLoginDTO
            {
                IdLogin = login.IdLogin,
                Nome = login.Nome,
                Email = login.Email,
                PasswordHash = ""
            };

            return retornoDTO;
        }

        public async Task<LerLoginDTO> ObterLoginPorEmail(string email) // pensar em como implementar
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LerLoginDTO>> ObterLogins()
        {
            List<LerLoginDTO> logins = new List<LerLoginDTO>();
            foreach (var login in await _repositorio.ObterLogins())
            {
                logins.Add(new LerLoginDTO
                {
                    IdLogin = login.IdLogin,
                    Nome = login.Nome,
                    Email = login.Email,
                    PasswordHash = ""
                });
            }
            return logins;
        }
    }
}
