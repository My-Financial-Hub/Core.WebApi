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
