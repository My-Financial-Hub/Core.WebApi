using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using FinancialHub.CQRS;

using FinancialHub.Transactions.Application.UseCases.Categories.Delete;

namespace FinancialHub.Transactions.WebApi.Controllers.Categories
{
    [Tags("Categories")]
    public sealed class DeleteController : BaseCategoriesController
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly ILogger<DeleteController> logger;

        public DeleteController(ICommandDispatcher commandDispatcher, ILogger<DeleteController> logger)
        {
            this.commandDispatcher = commandDispatcher;
            this.logger = logger;
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            this.logger.LogInformation("Starting deletion of category {CategoryId}", id);

            var command = new DeleteCategoryCommand(id);
            await this.commandDispatcher.DispatchAsync(command);

            this.logger.LogInformation("Finished deletion of category {CategoryId}", id);

            return this.NoContent();
        }
    }
}
