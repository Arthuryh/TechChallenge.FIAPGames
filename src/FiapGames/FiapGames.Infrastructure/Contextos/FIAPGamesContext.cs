using FiapGames.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FiapGames.Infrastructure.Contextos
{
    public class FIAPGamesContext : DbContext
    {
        public FIAPGamesContext(DbContextOptions<FIAPGamesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Login>()
                .HasOne(login => login.Conta)
                .WithOne(conta => conta.Login)
                .HasForeignKey<Login>(login => login.IdConta);

            builder.Entity<Conta>()
                .HasOne(conta => conta.Login)
                .WithOne(login => login.Conta)
                .HasForeignKey<Conta>(conta => conta.IdLogin);

            builder.Entity<Jogo>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Nome).HasMaxLength(150).IsRequired();
                entity.Property(x => x.Preco).HasColumnType("decimal(10,2)");

                entity.HasOne(x => x.Promocao)
                      .WithMany()
                      .HasForeignKey(x => x.PromocaoId);
            });

            builder.Entity<Promocao>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            builder.Entity<Compra>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasMany(x => x.CompraJogos)
                      .WithOne()
                      .HasForeignKey(x => x.CompraId);
            });

            builder.Entity<CompraJogo>(entity =>
            {
                entity.HasKey(x => new { x.CompraId, x.JogoId });
            });
        }


        public DbSet<Login> Logins { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraJogo> CompraJogos { get; set; }
    }
}
