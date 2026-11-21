using System;

namespace FinancialHub.Transactions.Domain.Data.Create
{
    public sealed class TransactionCreateDates
    {
        public DateTimeOffset? TargetDate { get; set; }
        public DateTimeOffset? FinishDate { get; set; }
    }
}
