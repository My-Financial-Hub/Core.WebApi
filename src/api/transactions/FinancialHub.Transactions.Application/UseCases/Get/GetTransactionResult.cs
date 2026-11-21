using System.Collections.Generic;
using FinancialHub.CQRS.Results;
using FinancialHub.CQRS.Results.Errors;
using FinancialHub.Transactions.Domain;

namespace FinancialHub.Transactions.Application.UseCases.Get
{
    public sealed class GetTransactionResult : ServiceResult<IEnumerable<Transaction>>
    {
        public GetTransactionResult(IEnumerable<Transaction> data, ServiceError? error = null) : base(data, error) { }
    }
}
