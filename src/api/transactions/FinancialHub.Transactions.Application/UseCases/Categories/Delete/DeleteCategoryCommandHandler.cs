using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using FinancialHub.CQRS.Handlers.Commands;
using FinancialHub.Transactions.Domain.Interfaces;

namespace FinancialHub.Transactions.Application.UseCases.Categories.Delete
{
    internal sealed class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly ITransactionCategoryRepository repository;
        private readonly ILogger<DeleteCategoryCommandHandler> logger;

        public DeleteCategoryCommandHandler(
            ITransactionCategoryRepository repository,
            ILogger<DeleteCategoryCommandHandler> logger
        )
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task Handle(DeleteCategoryCommand command)
        {
            this.logger.LogInformation("Handling DeleteCategoryCommand for Category ID {CategoryId}.", command.CategoryId);

            var category = await this.repository.GetByIdAsync(command.CategoryId);

            if (category == null)
            {
                this.logger.LogWarning("Category with ID {CategoryId} not found for deletion.", command.CategoryId);
                return;
            }

            await this.repository.DeleteAsync(command.CategoryId);
            this.logger.LogInformation("Category with ID {CategoryId} deleted successfully.", command.CategoryId);
        }
    }
}
