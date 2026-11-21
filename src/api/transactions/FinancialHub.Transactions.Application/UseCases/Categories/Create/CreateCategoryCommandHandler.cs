using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using FinancialHub.CQRS.Handlers.Commands;

using FinancialHub.Transactions.Domain.Entities;
using FinancialHub.Transactions.Domain.Interfaces;

namespace FinancialHub.Transactions.Application.UseCases.Categories.Create
{
    internal sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
    {
        private readonly ITransactionCategoryRepository repository;
        private readonly ILogger<CreateCategoryCommandHandler> logger;

        public CreateCategoryCommandHandler(ITransactionCategoryRepository repository, ILogger<CreateCategoryCommandHandler> logger) 
        { 
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command)
        {
            logger.LogInformation("Creating category with name {CategoryName}", command.Name);
            var category = new TransactionCategory(command.Name);

            var result = await repository.AddAsync(category);

            logger.LogInformation("Category {CategoryName} created with ID {CategoryId}", result.Name, result.Id);
            return new CreateCategoryResult(result);
        }
    }
}
