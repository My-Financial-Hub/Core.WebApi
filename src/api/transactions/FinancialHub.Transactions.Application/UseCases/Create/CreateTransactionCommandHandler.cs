using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using FinancialHub.CQRS.Handlers.Commands;
using FinancialHub.CQRS.Results;

using FinancialHub.Transactions.Application.Errors;

using FinancialHub.Transactions.Domain;
using FinancialHub.Transactions.Domain.Data.Create;
using FinancialHub.Transactions.Domain.Interfaces;
using FinancialHub.Transactions.Domain.ValueObjects;

namespace FinancialHub.Transactions.Application.UseCases.Create
{
    internal class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, ServiceResult>
    {
        private readonly ITransactionRepository repository;
        private readonly ITransactionCategoryRepository categoryRepository;
        private readonly ITransactionBalanceRepository balanceRepository;
        private readonly ILogger<CreateTransactionCommandHandler> logger;

        public CreateTransactionCommandHandler(
            ITransactionRepository repository,
            ITransactionCategoryRepository categoryRepository,
            ITransactionBalanceRepository balanceRepository,
            ILogger<CreateTransactionCommandHandler> logger
        )
        {
            this.repository = repository;
            this.categoryRepository = categoryRepository;
            this.balanceRepository = balanceRepository;
            this.logger = logger;
        }

        public async Task<ServiceResult> Handle(CreateTransactionCommand command)
        {
            using var scope = this.logger.BeginScope(
                new Dictionary<string, object>
                {
                    ["Context"] = nameof(CreateTransactionCommandHandler),
                    ["Command"] = command,
                }
            );

            this.logger.LogDebug("Searching for balance {BalanceId}.", command.BalanceId);
            var balance = await this.balanceRepository.GetByIdAsync(command.BalanceId);
            if (balance is null)
            {
                this.logger.LogWarning("Transaction creation failed because the balance was not found.");
                return new BalanceNotFoundResultError(command.BalanceId);
            }

            this.logger.LogDebug("Searching for category {CategoryId}.", command.CategoryId);
            var category = await this.categoryRepository.GetByIdAsync(command.CategoryId);
            if (category is null)
            {
                this.logger.LogWarning("Transaction creation failed because the category was not found.");
                return new CategoryNotFoundResultError(command.CategoryId);
            }

            this.logger.LogInformation("Creating transaction");
            var transaction = Transaction.Create(
                new TransactionCreateData()
                {
                    Relations = new TransactionCreateRelations()
                    {
                        Balance = balance,
                        Category = category
                    },
                    Details = new TransactionCreateDetails()
                    {
                        Description = command.Description,
                        Amount = new Money(command.Amount),
                        Kind = command.Kind,
                        Status = command.Status
                    },
                    Dates = new TransactionCreateDates()
                    {
                        TargetDate = command.TargetDate,
                        FinishDate = command.FinishDate
                    }
                }
            );

            await this.repository.AddAsync(transaction);

            this.logger.LogInformation("Transaction created successfully.");

            return ServiceResult.Success();
        }
    }
}
