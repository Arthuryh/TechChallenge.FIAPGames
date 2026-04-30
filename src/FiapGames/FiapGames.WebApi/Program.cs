using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using FiapGames.Infrastructure.DI;
using FiapGames.Application.DI;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRepositorios();
        builder.Services.AddApplicationServices();

        var connectionString = builder.Configuration.GetConnectionString("FIAPGamesConnection");

        var key = Encoding.ASCII.GetBytes("MINHA_CHAVE_SUPER_SECRETA_COM_32_BYTES!!");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),

                RoleClaimType = System.Security.Claims.ClaimTypes.Role
            };
        });

        builder.Services.AddAuthorization();

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

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}