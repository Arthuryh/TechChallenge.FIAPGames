using FiapGames.Application.Interfaces;
using FiapGames.Application.Interfaces.Compra;
using FiapGames.Application.Interfaces.Jogo;
using FiapGames.Application.Interfaces.Login;
using FiapGames.Application.Servicos;
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

            return services;
        }
    }
}
