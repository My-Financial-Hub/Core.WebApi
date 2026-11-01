using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using FinancialHub.CQRS.Handlers.Queries;

using FinancialHub.Transactions.Domain.Interfaces;

namespace FinancialHub.Transactions.Application.UseCases.Get.ById
{
    internal sealed class GetTransactionByIdQueryHandler : IQueryHandler<GetTransactionByIdQuery, GetTransactionByIdResult>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ILogger<GetTransactionByIdQueryHandler> logger;

        public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository, ILogger<GetTransactionByIdQueryHandler> logger)
        {
            this.transactionRepository = transactionRepository;
            this.logger = logger;
        }

        public async Task<GetTransactionByIdResult> Handle(GetTransactionByIdQuery query)
        {
            using var scope = this.logger.BeginScope(
                new Dictionary<string, object>
                {
                    ["Context"] = nameof(GetTransactionByIdQueryHandler),
                    ["Query"] = query,
                }
            );
            this.logger.LogInformation("Searching transaction with ID {TransactionId}", query.TransactionId);
            var transaction = await transactionRepository.GetByIdAsync(query.TransactionId);
            if (transaction == null)
            {
                this.logger.LogWarning("Transaction with ID {TransactionId} not found.", query.TransactionId);
                return GetTransactionByIdResult.NotFound(query.TransactionId);
            }

            this.logger.LogInformation("Transaction with ID {TransactionId} retrieved successfully.", query.TransactionId);
            return new GetTransactionByIdResult(
                new GetTransactionByIdDto
                {
                    Id = transaction.Id,
                    Description = transaction.Description,
                    Balance = new GetTransactionBalanceByIdDto
                    {
                        Id = transaction.Balance.Id,
                        Name = transaction.Balance.Name
                    },
                    Category = new GetTransactionCategoryByIdDto
                    {
                        Id = transaction.Category.Id,
                        Name = transaction.Category.Name
                    },

                    Amount = transaction.Amount.Value,
                    Kind = transaction.Kind.ToString(),
                    Status = transaction.Status.ToString(),

                    TargetDate = transaction.TargetDate,
                    FinishDate = transaction.FinishDate
                }
            );
        }
    }
}
