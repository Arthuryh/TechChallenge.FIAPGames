using FiapGames.Application.DTOs.Login;
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
            string senhaHash = BCrypt.Net.BCrypt.HashPassword(loginDTO.PasswordHash);

            var novoLogin = new Login(loginDTO.Nome, loginDTO.Email, senhaHash, (int)loginDTO.TipoUsuario);
            
            await _repositorio.AdicionarLogin(novoLogin);

            return loginDTO;
        }

        public async Task<LerLoginDTO?> ObterLoginPorId(int id)
        {
            var login = await _repositorio.ObterLoginPorId(id);
            if (login == null) throw new ArgumentException("Login não encontrado", nameof(id));

            return new LerLoginDTO
            (
                login.IdLogin,
                login.Nome,
                login.Email,
                login.PasswordHash = "",
                login.Ativo ? "Sim" : "Não",
                login.TipoUsuario
            );
        }

        public async Task<LerLoginDTO> ObterLoginPorEmail(string email)
        {
            var login = await _repositorio.ObterLoginPorEmail(email);
            if (login == null) throw new ArgumentException("Login não encontrado");

            return new LerLoginDTO
            (
                login.IdLogin,
                login.Nome,
                login.Email,
                login.PasswordHash = "",
                login.Ativo ? "Sim" : "Não",
                login.TipoUsuario
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
                    login.TipoUsuario
                ));
            }
            return logins;
        }

        public async Task AtualizarLogin(AtualizarLoginDTO loginDTO)
        {
            var login = await _repositorio.ObterLoginPorId(loginDTO.IdLogin);
            string senhaHash = BCrypt.Net.BCrypt.HashPassword(loginDTO.PasswordHash);

            if (login == null) throw new ArgumentException("Login não encontrado");

            login.AtualizarLogin(loginDTO.Nome, loginDTO.Email, senhaHash); 
            await _repositorio.AtualizarLogin(login);
        }

        public async Task DeletarLogin(int id)
        {
            var login = await _repositorio.ObterLoginPorId(id);

            if (login == null) throw new ArgumentException("Login não encontrado");

            login.DesativarLogin();

            await _repositorio.AtualizarLogin(login);
        }

        public async Task<LerLoginDTO> ValidarCredenciaisAsync(LogarLoginDTO logarLogin)
        {
            var login = await _repositorio.ObterLoginPorEmail(logarLogin.Email);
            if (login == null)
            {
                throw new ArgumentException("Credenciais inválidas");
            }
            bool senhaValida = BCrypt.Net.BCrypt.Verify(logarLogin.PasswordHash, login.PasswordHash);
            if (!senhaValida)
            {
                throw new ArgumentException("Credenciais inválidas");
            }

            return new LerLoginDTO
            (
                login.IdLogin,
                login.Nome,
                login.Email,
                login.PasswordHash = "",
                login.Ativo ? "Sim" : "Não",
                login.TipoUsuario
            );
        }
    }
}
