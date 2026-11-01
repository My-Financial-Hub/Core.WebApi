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
            Balances = balances;
            Categories = categories;
            DateRange = dateRange;
            Statuses = statuses;
            Kinds = kinds;
        }
    }
}
