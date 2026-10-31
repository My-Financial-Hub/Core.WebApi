using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinancialHub.Transactions.Infra.Data.Common.Entities;

namespace FinancialHub.Transactions.Infra.Database.Mappings
{
    internal sealed class TransactionEntityMapping : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique(true);

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasMaxLength(500);
            builder.Property(t => t.Amount)
                .HasColumnType("money")
                .IsRequired(true);

            // Optional Dates
            builder.Property(t => t.TargetDate)
                .HasColumnName("target_date")
                .IsRequired(false);
            builder.Property(t => t.FinishDate)
                .HasColumnName("finish_date")
                .IsRequired(false);

            // Balance Relationship
            builder.Property(t => t.BalanceId)
                .HasColumnName("balance_id")
                .IsRequired(true);
            builder.HasOne(x => x.Balance)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.BalanceId)
                .HasPrincipalKey(x => x.Id);

            // Category Relationship
            builder.Property(t => t.CategoryId)
                .HasColumnName("category_id")
                .IsRequired(true);
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.CategoryId)
                .HasPrincipalKey(x => x.Id);

            builder.Property(t => t.IsActive)
                .HasColumnName("active")
                .IsRequired(true);
            builder.Property(t => t.Status)
                .HasColumnName("status")
                .IsRequired(true);
            builder.Property(t => t.Type)
                .HasColumnName("type")
                .IsRequired(true);

            builder.Property(t => t.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired(true);
            builder.Property(t => t.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired(false);

            builder.HasIndex(x => new { x.CategoryId, x.BalanceId, x.Type });
            builder.HasIndex(x => new { x.CategoryId, x.BalanceId });

            builder.HasIndex(x => new { x.CategoryId, x.Type });
            builder.HasIndex(x => new { x.BalanceId, x.Type });

            builder.HasIndex(x => new { x.CreatedAt });

            builder.ToTable("transactions");
        }
    }
}
