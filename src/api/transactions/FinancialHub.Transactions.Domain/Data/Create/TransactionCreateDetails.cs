using FinancialHub.Transactions.Domain.Enums;
using FinancialHub.Transactions.Domain.ValueObjects;

namespace FinancialHub.Transactions.Domain.Data.Create
{
    public sealed class TransactionCreateDetails
    {
        /// <summary>
        /// The transaction description.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// The transaction amount.
        /// </summary>
        public Money Amount { get; set; }

        /// <summary>
        /// The transaction kind.
        /// </summary>
        public TransactionKind Kind { get; set; }

        /// <summary>
        /// The status of the transaction.
        /// </summary>
        public TransactionStatus Status { get; set; }
    }
}
