using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialHub.Transactions.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        /// <summary>
        /// Async method to add a new transaction to the repository.
        /// </summary>
        /// <param name="transaction">The transaction entity to be added to the repository.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);


        /// <summary>
        /// Async method to retrieve a transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction to retrieve.</param>
        /// <returns>A <see cref="Task{Transaction}"/> representing the asynchronous operation.</returns>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        Task<Transaction> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Transaction>> GetAsync(TransactionFilter filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Async method to update an existing transaction in the repository.
        /// </summary>
        /// <param name="transaction">The transaction entity with updated information to be saved in the repository.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken = default);

        /// <summary>
        /// Async method to delete a transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction to delete.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }

    public sealed class TransactionFilter
    {
        public TransactionFilter(
            Guid[]? balances = null,
            Guid[]? categories = null,
            (DateTime StartDate, DateTime EndDate)? dateRange = null,
            string[]? statuses = null,
            string[]? kinds = null
        )
        {
            this.Balances = balances;
            this.Categories = categories;
            this.DateRange = dateRange;
            this.Statuses = statuses;
            this.Kinds = kinds;
        }
        public Guid[]? Balances { get; }
        public Guid[]? Categories { get; }
        public (DateTime StartDate, DateTime EndDate)? DateRange { get; }
        public string[]? Statuses { get; }
        public string[]? Kinds { get; }
    }
}