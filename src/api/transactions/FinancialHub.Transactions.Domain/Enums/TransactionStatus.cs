namespace FinancialHub.Transactions.Domain.Enums
{
    /// <summary>
    /// Represents the lifecycle state of a transaction.
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// The transaction has been created but not finalized.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// The transaction has been finalized.
        /// </summary>
        Completed = 2
    }
}
