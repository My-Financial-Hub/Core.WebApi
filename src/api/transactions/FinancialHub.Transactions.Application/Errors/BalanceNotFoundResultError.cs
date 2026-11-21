using System;
using FinancialHub.CQRS.Results.Errors;

namespace FinancialHub.Transactions.Application.Errors
{
    internal class BalanceNotFoundResultError : ServiceError
    {
        public BalanceNotFoundResultError(Guid balanceId) : base($"Balance ${balanceId} not found") { }
    }
}
