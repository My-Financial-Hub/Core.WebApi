using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using FinancialHub.CQRS.Handlers.Commands;

using FinancialHub.Transactions.Domain;
using FinancialHub.Transactions.Domain.Entities;
using FinancialHub.Transactions.Domain.Interfaces;

namespace FinancialHub.Transactions.Application.UseCases.Update
{
    internal class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand, UpdateTransactionResult>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ITransactionBalanceRepository balanceRepository;
        private readonly ITransactionCategoryRepository categoryRepository;

        private readonly ILogger<UpdateTransactionCommandHandler> logger;

        public UpdateTransactionCommandHandler(
            ITransactionRepository transactionRepository, 
            ITransactionBalanceRepository balanceRepository, 
            ITransactionCategoryRepository categoryRepository,
            ILogger<UpdateTransactionCommandHandler> logger
        )
        {
            this.transactionRepository = transactionRepository;
            this.balanceRepository = balanceRepository;
            this.categoryRepository = categoryRepository;

            this.logger = logger;
        }

        public async Task<UpdateTransactionResult> Handle(UpdateTransactionCommand command)
        {
            var transaction = await transactionRepository.GetByIdAsync(command.Id);
            if (transaction == null)
            {
                logger.LogWarning("Transaction with ID {TransactionId} not found.", command.Id);
                return UpdateTransactionResult.TransactionNotFound(command.Id);
            }

            TransactionBalance? balance = null;
            if (command.BalanceId.HasValue)
            {
                var balanceId = command.BalanceId.Value;
                balance = await this.balanceRepository.GetByIdAsync(balanceId);
                if (balance == null)
                {
                    this.logger.LogWarning("Balance with ID {BalanceId} not found.", balanceId);
                    return UpdateTransactionResult.BalanceNotFound(balanceId);
                }
            }

            TransactionCategory? category = null;
            if (command.CategoryId.HasValue)
            {
                var categoryId = command.CategoryId.Value;
                category = await this.categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    this.logger.LogWarning("Category with ID {CategoryId} not found.", categoryId);
                    return UpdateTransactionResult.CategoryNotFound(categoryId);
                }
            }

            transaction.Update(
                balance,
                category,
                command.Amount,
                command.Description,
                command.Kind,
                command.Status,
                command.TargetDate,
                command.FinishDate
            );

            await this.transactionRepository.UpdateAsync(transaction);

            return new UpdateTransactionResult();
        }
    }
}
