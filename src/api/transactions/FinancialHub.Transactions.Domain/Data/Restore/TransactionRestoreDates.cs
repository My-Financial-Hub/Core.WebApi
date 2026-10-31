using System;

namespace FinancialHub.Transactions.Domain.Data.Restore
{
    public sealed class TransactionRestoreDates
    {

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public DateTimeOffset? TargetDate { get; set; }
        public DateTimeOffset? FinishDate { get; set; }
    }
}
