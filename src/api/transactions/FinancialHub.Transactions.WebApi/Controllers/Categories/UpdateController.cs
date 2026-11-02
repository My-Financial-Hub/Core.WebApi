using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;

using FinancialHub.CQRS;
using FinancialHub.CQRS.Results.Errors;

using FinancialHub.Transactions.WebApi.Requests;

using FinancialHub.Transactions.Application.UseCases.Categories.Update;
using FinancialHub.Transactions.WebApi.Requests.Categories;

namespace FinancialHub.Transactions.WebApi.Controllers.Categories
{
    [Tags("Categories")]
    public sealed class UpdateController : BaseTransactionsController
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly ILogger<UpdateController> logger;

        public UpdateController(ICommandDispatcher commandDispatcher, ILogger<UpdateController> logger)
        {
            this.commandDispatcher = commandDispatcher;
            this.logger = logger;
        }

        [HttpPatch("{id:guid}")]
        //[EndpointSummary("Updates a Category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ServiceError), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id, 
            [FromBody] UpdateCategoryRequestBody transaction
        )
        {
            var command = new UpdateCategoryCommand(id, transaction.Name);

            var result = await this.commandDispatcher.DispatchAsync<UpdateCategoryCommand, UpdateCategoryResult>(command);
            if (result.HasError)
            {
                if (result.Error?.Message == $"Category with id '{id}' not found.")
                    return NotFound();

                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
