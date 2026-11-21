using FinancialHub.Transactions.Domain.Enums;
using System;

namespace FinancialHub.Transactions.Application.UseCases.Update
{
    public sealed class UpdateTransactionCommand
    {
        public Guid Id { get; set; }
        public Guid? BalanceId { get; set; }
        public Guid? CategoryId { get; set; }

        public decimal? Amount { get; set; }
        public string? Description { get; set; } 

        public TransactionStatus? Status { get; set; }
        public TransactionKind? Kind { get; set; }

        public DateTime? TargetDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
