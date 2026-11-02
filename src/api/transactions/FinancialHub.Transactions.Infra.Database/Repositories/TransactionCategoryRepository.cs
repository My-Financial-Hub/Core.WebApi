using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FinancialHub.Transactions.Domain.Entities;
using FinancialHub.Transactions.Domain.Interfaces;
using FinancialHub.Transactions.Infra.Database.Context;
using FinancialHub.Transactions.Infra.Database.Mappers;

namespace FinancialHub.Transactions.Infra.Database.Repositories
{
    internal sealed class TransactionCategoryRepository : ITransactionCategoryRepository
    {
        private readonly TransactionsDbContext context;
        private readonly TransactionCategoryMapper mapper;
        private readonly ILogger<TransactionCategoryRepository> logger;

        public TransactionCategoryRepository(
            TransactionsDbContext context,
            TransactionCategoryMapper mapper,
            ILogger<TransactionCategoryRepository> logger
        )
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">Thrown when the category is null.</exception>
        public async Task<TransactionCategory> AddAsync(TransactionCategory category, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(category);

            this.logger.LogTrace("Adding category to the database.");
            var entity = this.mapper.ToEntity(category);

            var result = await this.context.Categories.AddAsync(entity, cancellationToken);

            await this.context.SaveChangesAsync(cancellationToken);
            this.logger.LogTrace("Category added successfully.");

            return this.mapper.FromEntity(result.Entity);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">Thrown when the category is null.</exception>
        public async Task<TransactionCategory> UpdateAsync(TransactionCategory category, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(category);

            this.logger.LogTrace("Updating category with id {Id}.", category.Id);
            var entity = this.mapper.ToEntity(category);

            var result = this.context.Update(entity);

            await this.context.SaveChangesAsync(cancellationToken);
            this.logger.LogTrace("Category with id {Id} updated successfully.", category.Id);

            return this.mapper.FromEntity(result.Entity);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            this.logger.LogTrace("Deleting category with id {Id}.", id);
            var entity = await this.context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (entity == null)
            {
                this.logger.LogTrace("Category with id {Id} not found.", id);
                return;
            }

            context.Categories.Remove(entity);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogTrace("Category with id {Id} deleted successfully.", id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TransactionCategory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            this.logger.LogTrace("Fetching all categories.");
            var entities = await this.context.Categories
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
            this.logger.LogTrace("Fetched {Count} categories.", entities.Count);
            return entities.Select(this.mapper.FromEntity);
        }

        /// <inheritdoc />
        public async Task<TransactionCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            this.logger.LogTrace("Fetching category with id {Id}.", id);
            var entity = await this.context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            if (entity == null)
            {
                this.logger.LogError("Category with id {Id} not found.", id);
                return null;
            }

            this.logger.LogTrace("Category with id {Id} fetched successfully.", id);
            return this.mapper.FromEntity(entity);
        }
    }
}
