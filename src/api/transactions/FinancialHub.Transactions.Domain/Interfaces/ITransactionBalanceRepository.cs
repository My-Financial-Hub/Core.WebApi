using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FinancialHub.Transactions.Domain.Entities;

namespace FinancialHub.Transactions.Domain.Interfaces
{
    public interface ITransactionBalanceRepository
    {
        Task<IEnumerable<TransactionBalance>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TransactionBalance> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
