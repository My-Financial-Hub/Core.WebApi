using System;
using FinancialHub.Transactions.Domain.Enums;

namespace FinancialHub.Transactions.Application.UseCases.Get
{
    public sealed class GetTransactionQuery
    {
        public Guid[]? Balances { get; }
        public Guid[]? Categories { get; }
        public (DateTime StartDate, DateTime EndDate)? DateRange { get; }
        public TransactionStatus[]? Statuses { get; }
        public TransactionKind[]? Kinds { get; }

        public GetTransactionQuery(
            Guid[]? balances = null,
            Guid[]? categories = null,
            (DateTime StartDate, DateTime EndDate)? dateRange = null,
            TransactionStatus[]? statuses = null,
            TransactionKind[]? kinds = null
        )
        {
            this.Balances = balances;
            this.Categories = categories;
            this.DateRange = dateRange;
            this.Statuses = statuses;
            this.Kinds = kinds;
        }
    }
}
