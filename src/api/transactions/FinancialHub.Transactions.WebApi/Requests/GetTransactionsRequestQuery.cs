using FinancialHub.Transactions.Domain.Enums;

namespace FinancialHub.Transactions.WebApi.Requests
{
    public sealed class GetTransactionsRequestQuery
    {
        public Guid[]? Balances { get; set; }
        public Guid[]? Categories { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TransactionKind[]? Kinds { get; set; }
        public TransactionStatus[]? Statuses { get; set; }
    }
}
