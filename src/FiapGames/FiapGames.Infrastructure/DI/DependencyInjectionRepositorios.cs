using FiapGames.Infrastructure.Interfaces;
using FiapGames.Infrastructure.Interfaces.LogRepo;
using FiapGames.Infrastructure.Repositorios;
using FiapGames.Infrastructure.Repositorios.LogsRepo;
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
            services.AddScoped<IContaRepositorio, ContaRepositorio>();

            return services;
        }
    }
}
