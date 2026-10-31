namespace FinancialHub.Transactions.Domain.Exceptions
{
    public sealed class InvalidTransactionBalanceException : TransactionDomainException
    {
        public InvalidTransactionBalanceException(string message) : base(message)
        {
        }
    }
}
