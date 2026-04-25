using FiapGames.Application.Interfaces;
using FiapGames.Application.Interfaces.Compra;
using FiapGames.Application.Interfaces.Jogo;
using FiapGames.Application.Interfaces.Login;
using FiapGames.Application.Servicos;
using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces;
using FiapGames.Infrastructure.Middlewares;
using FiapGames.Infrastructure.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static FiapGames.Infrastructure.Middlewares.ExceptionMiddleware;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("FIAPGamesConnection");

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var erro = new
                {
                    erro = "Requisição inválida",
                    status = 400,
                    detalhes = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Value.Errors.Select(e => e.ErrorMessage)
                        )
                };

                return new BadRequestObjectResult(erro);
            };
        });

        builder.Services.AddDbContext<FIAPGamesContext>(opts =>
            opts
            .UseLazyLoadingProxies()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();
        builder.Services.AddScoped<ILoginServico, LoginServico>();

        builder.Services.AddScoped<IJogoRepositorio, JogoRepositorio>();
        builder.Services.AddScoped<IJogoServico, JogoServico>();

        builder.Services.AddScoped<ICompraRepositorio, CompraRepositorio>();
        builder.Services.AddScoped<ICompraServico, CompraServico>();

        builder.Services.AddScoped<IPromocaoRepositorio, PromocaoRepositorio>();
        builder.Services.AddScoped<IPromocaoServico, PromocaoServico>();

        // Add services to the container.

        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var erros = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors
                        .Select(e => $"{x.Key}: {e.ErrorMessage}"))
                        .ToList();

                    return new BadRequestObjectResult(new
                    {
                        erro = "Requisição inválida",
                        status = (int)HttpStatusCode.BadRequest,
                        detalhes = erros
                    });
                };
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();


        app.Run();
    }
}