using FiapGames.Application.DTOs.Login;
using FiapGames.Application.Interfaces.Login;
using FiapGames.Infrastructure.Interfaces;
using FiapGames.Domain.Entidades;

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
            
            var novoLogin = new Login(loginDTO.Nome, loginDTO.Email, loginDTO.PasswordHash, (int)loginDTO.TipoUsuario);

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
                (int)login.TipoUsuario
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

            if (login == null) throw new ArgumentException("Login não encontrado");

            login.AtualizarLogin(loginDTO.Nome, loginDTO.Email, loginDTO.PasswordHash);

            await _repositorio.AtualizarLogin(login);
        }

        public async Task DeletarLogin(int id)
        {
            var login = await _repositorio.ObterLoginPorId(id);

            if (login == null) throw new ArgumentException("Login não encontrado");

            login.DesativarLogin();

            await _repositorio.AtualizarLogin(login);
        }
    }
}
