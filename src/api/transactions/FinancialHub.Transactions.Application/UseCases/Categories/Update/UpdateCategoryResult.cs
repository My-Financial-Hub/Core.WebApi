using System;

using FinancialHub.CQRS.Results;
using FinancialHub.CQRS.Results.Errors;

using FinancialHub.Transactions.Domain.Entities;

namespace FinancialHub.Transactions.Application.UseCases.Categories.Update
{
    public sealed class UpdateCategoryResult : ServiceResult<TransactionCategory>
    {
        private UpdateCategoryResult(ServiceError error) : base(error)
        {
        }

        private UpdateCategoryResult(TransactionCategory data, ServiceError? error = null) : base(data, error)
        {
        }

        internal static UpdateCategoryResult Success(TransactionCategory category)
        {
            return new UpdateCategoryResult(category);
        }

        internal static UpdateCategoryResult NotFound(Guid categoryId)
        {
            return new UpdateCategoryResult(new NotFoundError($"Category with id '{categoryId}' not found."));
        }
    }
}
