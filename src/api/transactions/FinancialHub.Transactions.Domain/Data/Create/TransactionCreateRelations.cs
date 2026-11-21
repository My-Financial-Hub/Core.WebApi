using FinancialHub.Transactions.Domain.Entities;

namespace FinancialHub.Transactions.Domain.Data.Create
{
    public sealed class TransactionCreateRelations
    {
        /// <summary>
        /// Gets the balance that owns the transaction.
        /// </summary>
        public TransactionBalance Balance { get; set; }
        /// <summary>
        /// Gets the category assigned to the transaction.
        /// </summary>
        public TransactionCategory Category { get; set; }
    }
}
