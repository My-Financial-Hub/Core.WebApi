using System.Collections.Generic;

using FinancialHub.CQRS.Results;

using FinancialHub.Transactions.Domain.Entities;

namespace FinancialHub.Transactions.Application.UseCases.Get.Balances
{
    public sealed class GetTransactionBalancesResult : ServiceResult<IEnumerable<TransactionBalance>>
    {
        public GetTransactionBalancesResult(IEnumerable<TransactionBalance> data) : base(data, null) { }
    }
}
