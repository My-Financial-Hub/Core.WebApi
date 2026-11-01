using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FinancialHub.CQRS.Handlers.Queries;
using FinancialHub.Transactions.Domain.Interfaces;

namespace FinancialHub.Transactions.Application.UseCases.Get.Categories
{
    internal class GetTransactionCategoriesQueryHandler : IQueryHandler<GetTransactionCategoriesQuery, GetTransactionCategoriesResult>
    {
        private readonly ITransactionCategoryRepository repository;
        private readonly ILogger<GetTransactionCategoriesQueryHandler> logger;

        public GetTransactionCategoriesQueryHandler(ITransactionCategoryRepository repository, ILogger<GetTransactionCategoriesQueryHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<GetTransactionCategoriesResult> Handle(GetTransactionCategoriesQuery query)
        {
            var categories = await repository.GetAllAsync();

            return new GetTransactionCategoriesResult(categories);
        }
    }
}
