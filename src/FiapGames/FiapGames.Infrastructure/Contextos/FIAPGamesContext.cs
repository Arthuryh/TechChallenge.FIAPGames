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
        }
        
        
        public DbSet<Login> Logins { get; set; }
        public DbSet<Conta> Contas { get; set; }
    }
}
