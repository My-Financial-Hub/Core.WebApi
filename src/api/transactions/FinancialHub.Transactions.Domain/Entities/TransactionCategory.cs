using System;
using FinancialHub.Transactions.Domain.Exceptions;

namespace FinancialHub.Transactions.Domain.Entities
{
    /// <summary>
    /// Represents a business classification used to group transactions.
    /// </summary>
    public sealed class TransactionCategory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCategory"/> class.
        /// </summary>
        /// <param name="id">The category identifier.</param>
        /// <param name="name">The category display name.</param>
        public TransactionCategory(Guid id, string name)
        {
            if (id == Guid.Empty)
                throw new InvalidTransactionCategoryException("Category id is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidTransactionCategoryException("Category name is required.");

            this.Id = id;
            this.Name = name.Trim();
        }

        /// <summary>
        /// Gets the category identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the category display name.
        /// </summary>
        public string Name { get; }
    }
}
