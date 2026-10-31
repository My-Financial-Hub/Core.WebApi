namespace FinancialHub.Transactions.Domain.Enums
{
    /// <summary>
    /// Identifies whether a transaction brings money in or sends money out.
    /// </summary>
    public enum TransactionKind
    {
        /// <summary>
        /// Money entering an account.
        /// </summary>
        Earn = 1,

        /// <summary>
        /// Money leaving an account.
        /// </summary>
        Expense = 2
    }
}
