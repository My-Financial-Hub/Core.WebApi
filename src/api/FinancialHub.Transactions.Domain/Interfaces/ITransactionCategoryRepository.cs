using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using FinancialHub.Transactions.Domain.Entities;

namespace FinancialHub.Transactions.Domain.Interfaces
{
    public interface ITransactionCategoryRepository
    {
        Task<IEnumerable<TransactionCategory>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TransactionCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
