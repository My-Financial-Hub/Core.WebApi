using System.Collections.Generic;
using FinancialHub.CQRS.Results;
using FinancialHub.Transactions.Domain.Entities;

namespace FinancialHub.Transactions.Application.UseCases.Categories.Get
{
    public sealed class GetTransactionCategoriesResult : ServiceResult<IEnumerable<TransactionCategory>>
    {
        public GetTransactionCategoriesResult(IEnumerable<TransactionCategory> data) : base(data, null) { }
    }
}
