namespace FinancialHub.Transactions.Domain.Data.Create
{
    public sealed class TransactionCreateData
    {
        public TransactionCreateRelations Relations { get; set ; }
        public TransactionCreateDetails Details { get; set; }
        public TransactionCreateDates Dates { get; set; }
    }
}
