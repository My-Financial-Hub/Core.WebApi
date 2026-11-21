using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FinancialHub.Transactions.Domain;
using FinancialHub.Transactions.Domain.Interfaces;

using FinancialHub.Transactions.Infra.Database.Context;
using FinancialHub.Transactions.Infra.Database.Mappers;

namespace FinancialHub.Transactions.Infra.Database.Repositories
{
    internal sealed class TransactionsRepository : ITransactionRepository
    {
        private readonly TransactionsDbContext context;
        private readonly TransactionMapper mapper;
        private readonly ILogger<TransactionsRepository> logger;

        public TransactionsRepository(
            TransactionsDbContext context,
            TransactionMapper mapper,
            ILogger<TransactionsRepository> logger
        )
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(transaction);

            this.logger.LogTrace("Adding transaction to the database.");
            var entity = this.mapper.ToEntity(transaction);

            this.context.Entry(entity.Balance).State = EntityState.Unchanged;
            this.context.Entry(entity.Category).State = EntityState.Unchanged;

            await this.context.Transactions.AddAsync(entity, cancellationToken);

            await this.context.SaveChangesAsync(cancellationToken);
            this.logger.LogTrace("Transaction added successfully.");
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await this.context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (entity == null) 
                return;

            context.Transactions.Remove(entity);
            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Transaction>> GetAsync(TransactionFilter filter, CancellationToken cancellationToken = default)
        {
            return await this.context.Transactions
                .AsNoTracking()
                .Where(t =>
                    (filter.Balances == null || filter.Balances.Contains(t.BalanceId)) &&
                    (filter.Categories == null || filter.Categories.Contains(t.CategoryId)) &&
                    (filter.DateRange == null || (t.TargetDate >= filter.DateRange.Value.StartDate && t.TargetDate <= filter.DateRange.Value.EndDate)) &&
                    (filter.Statuses == null || filter.Statuses.Any(status => status == t.Status)) &&
                    (filter.Kinds == null || filter.Kinds.Any(kind => kind == t.Type))
                )
                .OrderByDescending(t => t.TargetDate)
                .Include(t => t.Balance)
                .Include(t => t.Category)
                .Select(t => this.mapper.FromEntity(t))
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Transaction> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await this.context.Transactions
                .AsNoTracking()
                .Include(t => t.Balance)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t=> t.Id == id, cancellationToken);

            if (entity is null)
                return null;

            return this.mapper.FromEntity(entity);
        }

        public async Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(transaction);

            var entity = this.mapper.ToEntity(transaction);

            this.context.Entry(entity.Balance).State = EntityState.Unchanged;
            this.context.Entry(entity.Category).State = EntityState.Unchanged;

            this.context.Update(entity);

            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
