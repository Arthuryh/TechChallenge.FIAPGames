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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Tabela Login
            builder.Entity<Login>(entity =>
            {
                //Propriedades Login
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

                //Relacionamento 1:1 com Conta
                entity.HasOne(e => e.Conta)
                    .WithOne(c => c.Login)
                    .HasForeignKey<Conta>(c => c.IdLogin)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //Tabela Conta
            builder.Entity<Conta>(entity =>
            {
                entity.HasKey(e => e.IdConta);
                entity.Property(e => e.Saldo)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();
                entity.Property(e => e.DataAtualizacao)
                    .IsRequired();
            });
        }
    }
}
