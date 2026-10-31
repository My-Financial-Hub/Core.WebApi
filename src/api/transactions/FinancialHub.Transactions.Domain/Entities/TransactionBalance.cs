using System;
using FinancialHub.Transactions.Domain.Exceptions;

namespace FinancialHub.Transactions.Domain.Entities
{
    /// <summary>
    /// Represents a financial balance that owns transactions.
    /// </summary>
    public sealed class TransactionBalance
    {
        /// <summary>
        /// Gets the balance identifier.
        /// </summary>
        public Guid Id { get; }
        /// <summary>
        /// Gets the balance display name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionBalance"/> class with a new unique identifier.
        /// </summary>
        /// <param name="name">The balance display name.</param>
        public TransactionBalance(string name) : this(Guid.NewGuid(), name) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionBalance"/> class.
        /// </summary>
        /// <param name="id">The balance identifier.</param>
        /// <param name="name">The balance display name.</param>
        public TransactionBalance(Guid id, string name)
        {
            if (id == Guid.Empty)
                throw new InvalidTransactionBalanceException("Balance id is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidTransactionBalanceException("Balance name is required.");

            Id = id;
            Name = name.Trim();
        }
    }
}
