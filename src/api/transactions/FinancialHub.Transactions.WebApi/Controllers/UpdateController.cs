using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;

using FinancialHub.CQRS;
using FinancialHub.CQRS.Results.Errors;

using FinancialHub.Transactions.WebApi.Requests;

using FinancialHub.Transactions.Application.UseCases.Update;

namespace FinancialHub.Transactions.WebApi.Controllers
{
    [Tags("Transactions")]
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
        //[EndpointSummary("Updates a transaction")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ServiceError), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id, 
            [FromBody] UpdateTransactionRequestBody transaction
        )
        {
            var command = new UpdateTransactionCommand()
            {
                Id = id,
                BalanceId = transaction.BalanceId,
                CategoryId = transaction.CategoryId,

                Amount = transaction.Amount,
                Description = transaction.Description,

                Status = transaction.Status,
                Kind = transaction.Kind,

                TargetDate = transaction.TargetDate,
                FinishDate = transaction.FinishDate
            };

            var result = await this.commandDispatcher
                .DispatchAsync<UpdateTransactionCommand, UpdateTransactionResult>(command);
            if (result.HasError)
            {
                if (result.Error?.Message == $"Transaction with id '{id}' not found.")
                    return NotFound();

                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
