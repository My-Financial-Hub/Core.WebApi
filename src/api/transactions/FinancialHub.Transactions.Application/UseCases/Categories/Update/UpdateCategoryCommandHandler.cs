using FinancialHub.CQRS.Handlers.Commands;
using FinancialHub.Transactions.Application.UseCases.Update;
using FinancialHub.Transactions.Domain.Entities;
using FinancialHub.Transactions.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FinancialHub.Transactions.Application.UseCases.Categories.Update
{
    internal sealed class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResult>
    {
        private readonly ITransactionCategoryRepository repository;
        private readonly ILogger<UpdateCategoryCommandHandler> logger;

        public UpdateCategoryCommandHandler(ITransactionCategoryRepository repository, ILogger<UpdateCategoryCommandHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand command)
        {
            if (await this.repository.GetByIdAsync(command.Id) == null)
            {
                this.logger.LogWarning("Category with ID {CategoryId} not found.", command.Id);
                return UpdateCategoryResult.NotFound(command.Id);
            }

            logger.LogInformation("Updating category with Id {CategoryId}", command.Id);
            var category = new TransactionCategory(command.Name);

            var result = await repository.UpdateAsync(category);

            logger.LogInformation("Category {CategoryId} updated with name {CategoryName}", result.Id, result.Name);
            return UpdateCategoryResult.Success(result);
        }
    }
}
