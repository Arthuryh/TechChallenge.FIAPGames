using FiapGames.Infrastructure.Interfaces;
using FiapGames.Infrastructure.Interfaces.Log;
using FiapGames.Infrastructure.Repositorios;
using FiapGames.Infrastructure.Repositorios.Logs;
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
