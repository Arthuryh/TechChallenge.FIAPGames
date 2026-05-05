using FiapGames.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FiapGames.Infrastructure.Contextos
{
    public class FIAPGamesContext : DbContext
    {
        public FIAPGamesContext(DbContextOptions<FIAPGamesContext> options) : base(options)
        {
        }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraJogo> CompraJogos { get; set; }
        public DbSet<LogMensagem> Logs { get; set; }
        public DbSet<Biblioteca> Bibliotecas { get; set; }
        public DbSet<BibliotecaJogo> BibliotecaJogos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.IdLogin);
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.HasIndex(e => e.Email)
                    .IsUnique();
                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.DataCriacao)
                    .IsRequired();
                entity.Property(e => e.Ativo)
                    .IsRequired();

                entity.HasOne(e => e.Conta)
                    .WithOne(c => c.Login)
                    .HasForeignKey<Conta>(c => c.IdLogin)
                    .OnDelete(DeleteBehavior.Cascade);



            });

            builder.Entity<Conta>(entity =>
            {
                entity.HasKey(e => e.IdConta);
                entity.Property(e => e.Saldo)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();
                entity.Property(e => e.DataAtualizacao)
                    .IsRequired();
            });

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

            builder.Entity<Biblioteca>(entity =>
            {
                entity.HasKey(x => x.IdBiblioteca);

                entity.HasMany(x => x.Jogos)
                      .WithOne()
                      .HasForeignKey("BibliotecaId")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<BibliotecaJogo>(entity =>
            {
                entity.HasKey(x => new { x.BibliotecaId, x.JogoId });

                entity.Property(x => x.DataAdicao)
                      .IsRequired();

                entity.HasOne<Jogo>()
                      .WithMany()
                      .HasForeignKey(x => x.JogoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
