using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FinancialHub.Transactions.Infra.Database.Context;
using FinancialHub.Transactions.Infra.Database.Mappers;
using FinancialHub.Transactions.Domain.Entities;
using FinancialHub.Transactions.Domain.Interfaces;

namespace FinancialHub.Transactions.Infra.Database.Repositories
{
    internal sealed class TransactionBalanceRepository : ITransactionBalanceRepository
    {
        private readonly TransactionsDbContext context;
        private readonly TransactionBalanceMapper mapper;
        private readonly ILogger<TransactionBalanceRepository> logger;

        public TransactionBalanceRepository(
            TransactionsDbContext context,
            TransactionBalanceMapper mapper,
            ILogger<TransactionBalanceRepository> logger
        )
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TransactionBalance>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            this.logger.LogTrace("Fetching all balances.");
            var entities = await this.context.Balances
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
            this.logger.LogTrace("Fetched {Count} balances.", entities.Count);

            return entities.Select(this.mapper.FromEntity);
        }

        /// <inheritdoc />
        public async Task<TransactionBalance?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            this.logger.LogTrace("Fetching balance with id {Id}.", id);
            var entity = await this.context.Balances
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            
            if (entity == null)
            {
                this.logger.LogError("Balance with id {Id} not found.", id);
                return null;
            }

            this.logger.LogTrace("Balance with id {Id} fetched successfully.", id);
            return this.mapper.FromEntity(entity);
        }
    }
}
