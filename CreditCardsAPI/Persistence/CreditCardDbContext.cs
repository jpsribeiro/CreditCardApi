using CreditCardsAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CreditCardsAPI.Persistence
{
    public class CreditCardDbContext : DbContext
    {
        public CreditCardDbContext(DbContextOptions<CreditCardDbContext> options) : base(options)
        {

        }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<VirtualCreditCard> VirtualCreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder) 
        {
            modelbuilder
                .Entity<Client>()
                .HasKey(x => x.Email);

            modelbuilder
                .Entity<CreditCard>()
                .HasKey(x => x.Number);

            modelbuilder
                .Entity<VirtualCreditCard>()
                .HasKey(x => x.Number);

            modelbuilder
                .Entity<Client>()
                .HasOne(x => x.CreditCard)
                .WithOne(x => x.Client)
                .HasForeignKey<CreditCard>(x => x.ClientEmail)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelbuilder
                .Entity<CreditCard>()
                .HasMany(x => x.VirtualCreditCards)
                .WithOne(x => x.CreditCard)
                .HasForeignKey(x => x.CreditCardNumber)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelbuilder
                .Entity<VirtualCreditCard>()
                .HasOne(x => x.CreditCard)
                .WithMany(x => x.VirtualCreditCards)
                .HasForeignKey(x => x.CreditCardNumber)
                .OnDelete(DeleteBehavior.ClientCascade);
        }

    }
}
