using FinancialHub.CQRS.Results;
using FinancialHub.CQRS.Results.Errors;
using FinancialHub.Transactions.Domain.Entities;

namespace FinancialHub.Transactions.Application.UseCases.Categories.Create
{
    public sealed class CreateCategoryResult : ServiceResult<TransactionCategory>
    {
        public CreateCategoryResult(TransactionCategory data, ServiceError? error = null) : base(data, error)
        {
        }
    }
}
