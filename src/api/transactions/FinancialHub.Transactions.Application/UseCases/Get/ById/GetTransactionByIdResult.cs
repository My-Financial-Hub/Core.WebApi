using System;
using FinancialHub.CQRS.Results;
using FinancialHub.CQRS.Results.Errors;

namespace FinancialHub.Transactions.Application.UseCases.Get.ById
{
    public sealed class GetTransactionCategoryByIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public sealed class GetTransactionBalanceByIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public sealed class GetTransactionByIdDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public GetTransactionCategoryByIdDto Category { get; set; }
        public GetTransactionBalanceByIdDto Balance { get; set; }

        public string Kind { get; set; }
        public string Status { get; set; }

        public DateTimeOffset? TargetDate { get; set; }
        public DateTimeOffset? FinishDate { get; set; }
    }

    public sealed class GetTransactionByIdResult : ServiceResult<GetTransactionByIdDto>
    {
        internal GetTransactionByIdResult(ServiceError error) : base(error)
        {
        }

        internal GetTransactionByIdResult(GetTransactionByIdDto data, ServiceError error = null) : base(data, error)
        {
        }

        internal static GetTransactionByIdResult NotFound(Guid id)
        {
            return new GetTransactionByIdResult(new NotFoundError($"Transaction with id '{id}' not found."));
        }
    }
}
