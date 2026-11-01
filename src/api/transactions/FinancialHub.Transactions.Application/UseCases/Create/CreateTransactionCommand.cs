using System;
using FinancialHub.Transactions.Domain.Enums;

namespace FinancialHub.Transactions.Application.UseCases.Create
{
    public sealed class CreateTransactionCommand
    {
        public string Description { get; set; }

        public Guid BalanceId { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Amount { get; set; }

        public TransactionKind Kind { get; set; }

        public TransactionStatus Status { get; set; }

        public DateTimeOffset? TargetDate { get ; set; }
        public DateTimeOffset? FinishDate { get ; set; }
    }
}
