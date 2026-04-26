using FiapGames.Application.Interfaces;
using FiapGames.Application.Interfaces.Compra;
using FiapGames.Application.Interfaces.Conta;
using FiapGames.Application.Interfaces.Jogo;
using FiapGames.Application.Interfaces.Login;
using FiapGames.Application.Servicos;
using FiapGames.Application.Servicos.Conta;
using FiapGames.Infrastructure.Contextos;
using FiapGames.Infrastructure.Interfaces;
using FiapGames.Infrastructure.Interfaces.Conta;
using FiapGames.Infrastructure.Repositorios;
using FiapGames.Infrastructure.Repositorios.Conta;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FIAPGamesConnection");

builder.Services.AddDbContext<FIAPGamesContext>(opts =>
    opts
    .UseLazyLoadingProxies()
    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();
builder.Services.AddScoped<ILoginServico, LoginServico>();

builder.Services.AddScoped<IContaRepositorio, ContaRepositorio>();
builder.Services.AddScoped<IContaServico, ContaServico>();

builder.Services.AddScoped<IJogoRepositorio, JogoRepositorio>();
builder.Services.AddScoped<IJogoServico, JogoServico>();

builder.Services.AddScoped<ICompraRepositorio, CompraRepositorio>();
builder.Services.AddScoped<ICompraServico, CompraServico>();

builder.Services.AddScoped<IPromocaoRepositorio, PromocaoRepositorio>();
builder.Services.AddScoped<IPromocaoServico, PromocaoServico>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


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
