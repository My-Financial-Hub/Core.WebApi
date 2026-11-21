
namespace FinancialHub.Transactions.Domain.Data.Restore
{
    public sealed class TransactionRestoreDetails
    {
        /// <summary>
        /// The transaction description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The transaction amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The transaction kind.
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// The status of the transaction.
        /// </summary>
        public string Status { get; set; }
    }
}
