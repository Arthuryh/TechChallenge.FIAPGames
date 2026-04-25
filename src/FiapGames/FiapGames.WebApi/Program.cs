using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using FiapGames.Infrastructure.DI;
using FiapGames.Application.DI;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRepositorios();
        builder.Services.AddApplicationServices();

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