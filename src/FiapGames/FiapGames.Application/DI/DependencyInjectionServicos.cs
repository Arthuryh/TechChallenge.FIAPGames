using FiapGames.Application.Interfaces;
using FiapGames.Application.Interfaces.Biblioteca;
using FiapGames.Application.Interfaces.Compra;
using FiapGames.Application.Interfaces.Conta;
using FiapGames.Application.Interfaces.Jogo;
using FiapGames.Application.Interfaces.Promocao;
using FiapGames.Application.Servicos;
using FiapGames.Application.Servicos.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace FiapGames.Application.DI
{
    public static class DependencyInjectionServicos
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPromocaoServico, PromocaoServico>();
            services.AddScoped<ILoginServico, LoginServico>();
            services.AddScoped<ICompraServico, CompraServico>();
            services.AddScoped<IJogoServico, JogoServico>();
            services.AddScoped<IContaServico, ContaServico>();
            services.AddScoped<ITokenServico, TokenServico>();
            services.AddScoped<IBibliotecaServico, BibliotecaServico>();

            return services;
        }
    }
}
