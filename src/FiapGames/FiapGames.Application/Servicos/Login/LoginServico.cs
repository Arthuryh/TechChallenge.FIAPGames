using FiapGames.Application.DTOs.Login;
using FiapGames.Application.Interfaces.Login;
using FiapGames.Domain.Entidades;
using FiapGames.Infrastructure.Interfaces.Login;

namespace FiapGames.Application.Servicos.Login
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
            var novoLogin = new Login(loginDTO.Nome, loginDTO.Email, loginDTO.PasswordHash, loginDTO.TipoUsuario);

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

            return new LerLoginDTO
            (
                login.IdLogin,
                login.Nome,
                login.Email,
                login.PasswordHash = "",
                login.Ativo ? "Sim" : "Não",
                (int)login.TipoUsuario
            );
        }

        public async Task<LerLoginDTO> ObterLoginPorEmail(string email) // revisar implementação
        {
            //validacao de email
            var login = await _repositorio.ObterLoginPorEmail(email);

            if (login == null) throw new Exception("Login não encontrado");

            // Mapear a Entidade de volta para o DTO
            return new LerLoginDTO
            (
                login.IdLogin,
                login.Nome,
                login.Email,
                login.PasswordHash = "",
                login.Ativo ? "Sim" : "Não",
                (int)login.TipoUsuario
            );
        }

        public async Task<IEnumerable<LerLoginDTO>> ObterLogins()
        {
            List<LerLoginDTO> logins = new List<LerLoginDTO>();
            foreach (var login in await _repositorio.ObterLogins())
            {
                logins.Add(new LerLoginDTO
                (
                    login.IdLogin,
                    login.Nome,
                    login.Email,
                    login.PasswordHash = "",
                    login.Ativo ? "Sim" : "Não",
                    (int)login.TipoUsuario
                ));
            }
            return logins;
        }

        public async Task AtualizarLogin(AtualizarLoginDTO loginDTO)
        {
            var login = await _repositorio.ObterLoginPorId(loginDTO.IdLogin);

            if (login == null) throw new Exception("Login não encontrado");

            login.AtualizarLogin(loginDTO.Nome, loginDTO.Email, loginDTO.PasswordHash);

            await _repositorio.AtualizarLogin(login);
        }

        public async Task DeletarLogin(int id)
        {
            var login = await _repositorio.ObterLoginPorId(id);

            if (login == null) throw new Exception("Login não encontrado");

            await _repositorio.DesativarLogin(id);
        }
    }
}
