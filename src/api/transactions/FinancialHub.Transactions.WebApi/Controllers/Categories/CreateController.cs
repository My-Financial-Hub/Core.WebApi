using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;

using FinancialHub.CQRS;

using FinancialHub.Transactions.WebApi.Requests.Categories;
using FinancialHub.Transactions.Application.UseCases.Categories.Create;

namespace FinancialHub.Transactions.WebApi.Controllers.Categories
{
    [Tags("Categories")]
    public class CreateController : BaseCategoriesController
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly ILogger<CreateController> logger;

        public CreateController(ICommandDispatcher commandDispatcher, ILogger<CreateController> logger)
        {
            this.commandDispatcher = commandDispatcher;
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequestBody category)
        {
            this.logger.LogInformation("Starting creation of category");
            var command = new CreateCategoryCommand(category.Name);
            var result = await this.commandDispatcher.DispatchAsync<CreateCategoryCommand, CreateCategoryResult>(command);
            if (result.HasError)
            {
                this.logger.LogWarning(
                    "Error creating category : {Message}",
                    result.Error!.Message
                );
                return this.BadRequest(result.Error);
            }
            this.logger.LogInformation("Finished creation of category");
            return this.Ok();
        }
    }
}
