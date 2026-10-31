using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinancialHub.Transactions.Infra.Data.Common.Entities;

namespace MyFinancialHub.Transactions.Infra.Database.Mappings
{
    internal sealed class CategoryEntityMapping : IEntityTypeConfiguration<TransactionCategoryEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionCategoryEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique(true);

            builder.Property(t => t.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.IsActive)
                .HasColumnName("active");

            builder.HasMany(t => t.Transactions)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId);

            builder.ToTable("categories");
        }
    }
}
