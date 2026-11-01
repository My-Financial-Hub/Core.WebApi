using System;
using FinancialHub.CQRS.Results;
using FinancialHub.CQRS.Results.Errors;

namespace FinancialHub.Transactions.Application.UseCases.Update
{
    public class UpdateTransactionResult : ServiceResult
    {
        internal UpdateTransactionResult() : base() { }

        internal UpdateTransactionResult(ServiceError? error) : base(error) { }

        internal static UpdateTransactionResult TransactionNotFound(Guid id)
        {
            return new UpdateTransactionResult(new NotFoundError($"Transaction with id '{id}' not found."));
        }

        internal static UpdateTransactionResult BalanceNotFound(Guid balanceId)
        {
            return new UpdateTransactionResult(new NotFoundError($"Balance with id '{balanceId}' not found."));
        }

        internal static UpdateTransactionResult CategoryNotFound(Guid categoryId)
        {
            return new UpdateTransactionResult(new NotFoundError($"Category with id '{categoryId}' not found."));
        }
    }
}
