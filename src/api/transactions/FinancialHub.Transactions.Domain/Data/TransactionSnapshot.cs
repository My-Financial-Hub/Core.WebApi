using System;
using FinancialHub.Transactions.Domain.Enums;

namespace FinancialHub.Transactions.Domain.Data
{
    public sealed class TransactionSnapshot
    {
        internal TransactionSnapshot(
            Guid balanceId, Guid categoryId, 
            decimal amount, string description, 
            TransactionKind kind, TransactionStatus status, 
            DateTimeOffset? targetDate, DateTimeOffset? finishDate
        )
        {
            this.BalanceId = balanceId;
            this.CategoryId = categoryId;
            this.Amount = amount;
            this.Description = description;
            this.Kind = kind;
            this.Status = status;
            this.TargetDate = targetDate;
            this.FinishDate = finishDate;
        }

        public Guid BalanceId { get; }
        public Guid CategoryId { get; }

        public decimal Amount { get; }
        public string Description { get; }

        public TransactionKind Kind { get; }
        public TransactionStatus Status { get; }

        public DateTimeOffset? TargetDate { get; }
        public DateTimeOffset? FinishDate { get; }
    }
}
