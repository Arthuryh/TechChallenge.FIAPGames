using FiapGames.Infrastructure.Interfaces.Compra;
using FiapGames.Infrastructure.Interfaces.Jogo;
using FiapGames.Infrastructure.Interfaces.Login;
using FiapGames.Infrastructure.Interfaces.LogRepo;
using FiapGames.Infrastructure.Interfaces.Promocao;
using FiapGames.Infrastructure.Repositorios.Compra;
using FiapGames.Infrastructure.Repositorios.Jogo;
using FiapGames.Infrastructure.Repositorios.Login;
using FiapGames.Infrastructure.Repositorios.LogsRepo;
using FiapGames.Infrastructure.Repositorios.Promocao;
using Microsoft.Extensions.DependencyInjection;

namespace FiapGames.Infrastructure.DI
{
    public static class DependencyInjectionRepositorios
    {
        public static IServiceCollection AddRepositorios(this IServiceCollection services)
        {
            services.AddScoped<ILoginRepositorio, LoginRepositorio>();
            services.AddScoped<IJogoRepositorio, JogoRepositorio>();
            services.AddScoped<ICompraRepositorio, CompraRepositorio>();
            services.AddScoped<IPromocaoRepositorio, PromocaoRepositorio>();
            services.AddScoped<ILogRepositorio, LogRepositorio>();

            return services;
        }
    }
}
