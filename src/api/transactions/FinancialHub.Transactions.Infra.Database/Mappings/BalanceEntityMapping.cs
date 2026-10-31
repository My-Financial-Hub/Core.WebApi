using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinancialHub.Transactions.Infra.Data.Common.Entities;

namespace FinancialHub.Transactions.Infra.Database.Mappings
{
    internal sealed class BalanceEntityMapping : IEntityTypeConfiguration<TransactionBalanceEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionBalanceEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique(true);

            builder.Property(t => t.Name)
                .HasColumnName("name")
                .HasMaxLength(200);

            builder.HasMany(t => t.Transactions)
                .WithOne(t => t.Balance)
                .HasForeignKey(t => t.BalanceId);

            builder.ToTable("balances");
        }
    }
}
