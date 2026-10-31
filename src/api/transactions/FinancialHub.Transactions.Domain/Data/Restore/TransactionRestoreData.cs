using System;

namespace FinancialHub.Transactions.Domain.Data.Restore
{
    public sealed class TransactionRestoreData
    {
        public Guid Id { get; set; }
        public TransactionRestoreRelations Relations { get; set; }
        public TransactionRestoreDetails Details { get; set; }
        public TransactionRestoreDates Dates { get; set; }
    }
}
