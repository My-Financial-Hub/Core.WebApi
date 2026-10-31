using System.Linq;
using Microsoft.EntityFrameworkCore;
using FinancialHub.Transactions.Infra.Database.Entities;
using FinancialHub.Transactions.Infra.Database.Mappings;

namespace FinancialHub.Transactions.Infra.Database.Context
{
    internal sealed class TransactionsDbContext : DbContext
    {
        internal DbSet<TransactionEntity> Transactions => this.Set<TransactionEntity>();

        internal IQueryable<TransactionCategoryEntity> Categories => this.Set<TransactionCategoryEntity>();

        internal IQueryable<TransactionBalanceEntity> Balances => this.Set<TransactionBalanceEntity>();

        public TransactionsDbContext() { }

        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityMapping());
            modelBuilder.ApplyConfiguration(new BalanceEntityMapping());
            modelBuilder.ApplyConfiguration(new TransactionEntityMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
