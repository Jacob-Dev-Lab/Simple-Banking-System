
using Microsoft.EntityFrameworkCore;
using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Data
{
    public class BankDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        //public DbSet<CurrentAccount> CurrentAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>()
                .HasDiscriminator<string>("AccountType")
                .HasValue<CurrentAccount>("Current")
                .HasValue<SavingsAccount>("Savings");

            builder.Entity<Account>()
                .Property(a => a.Balance)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
