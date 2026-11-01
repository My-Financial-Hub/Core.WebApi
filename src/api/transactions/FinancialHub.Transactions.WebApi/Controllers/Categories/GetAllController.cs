using FinancialHub.CQRS;
using FinancialHub.Transactions.Application.UseCases.Get.Categories;
using Microsoft.AspNetCore.Mvc;

namespace FinancialHub.Transactions.WebApi.Controllers.Categories
{
    [Tags("Categories")]
    public sealed class GetAllController : BaseCategoriesController
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ILogger<GetAllController> logger;
        public GetAllController(IQueryDispatcher queryDispatcher, ILogger<GetAllController> logger)
        {
            this.queryDispatcher = queryDispatcher;
            this.logger = logger;
        }

        [HttpGet]
        //[EndpointSummary("Gets all categories")]
        [ProducesResponseType(typeof(GetTransactionCategoriesResult), 200)]
        public async Task<IActionResult> GetAll()
        {
            this.logger.LogInformation("Starting searching for all categories");

            var query = new GetTransactionCategoriesQuery();
            var result = await queryDispatcher.DispatchAsync<GetTransactionCategoriesQuery, GetTransactionCategoriesResult>(query);
            
            this.logger.LogInformation("Finished searching for all categories");

            return Ok(result);
        }
    }
}
