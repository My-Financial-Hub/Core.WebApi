using System;
using FinancialHub.CQRS.Results.Errors;

namespace FinancialHub.Transactions.Application.Errors
{
    internal class CategoryNotFoundResultError : ServiceError
    {
        public CategoryNotFoundResultError(Guid categoryId) : base($"Category ${categoryId} not found") { }
    }
}
