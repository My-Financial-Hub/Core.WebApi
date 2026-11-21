using System;
using System.Threading;
using System.Threading.Tasks;

using FinancialHub.Transactions.Domain.Entities;

namespace FinancialHub.Transactions.Domain.Interfaces
{
    public interface ITransactionBalanceRepository
    {
        Task<TransactionBalance> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
