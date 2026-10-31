namespace FinancialHub.Transactions.Domain.Exceptions
{
    public sealed class InvalidTransactionCategoryException : TransactionDomainException
    {
        public InvalidTransactionCategoryException(string message) : base(message)
        {
        }
    }
}
