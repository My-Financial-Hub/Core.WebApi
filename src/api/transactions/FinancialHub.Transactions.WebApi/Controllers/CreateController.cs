using Microsoft.AspNetCore.Mvc;

using FinancialHub.CQRS;
using FinancialHub.CQRS.Results;
using FinancialHub.CQRS.Results.Errors;

using FinancialHub.Transactions.WebApi.Requests;
using FinancialHub.Transactions.Application.UseCases.Create;

namespace FinancialHub.Transactions.WebApi.Controllers
{
    [Tags("Transactions")]
    public sealed class CreateController(ICommandDispatcher commandDispatcher, ILogger<CreateController> logger) : BaseController
    {
        private readonly ICommandDispatcher commandDispatcher = commandDispatcher;
        private readonly ILogger<CreateController> logger = logger;

        [HttpPost]
        [EndpointSummary("Creates a transaction")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ServiceError), 404)]
        public async Task<IActionResult> Create([FromBody] CreateTransactionRequestBody transaction)
        {
            this.logger.LogInformation("Starting creation of transaction");
            var command = new CreateTransactionCommand()
            {
                BalanceId = transaction.BalanceId,
                CategoryId = transaction.CategoryId,
                Amount = transaction.Amount,
                Kind = transaction.Kind,
                Status = transaction.Status,
                TargetDate = transaction.TargetDate.HasValue? new DateTimeOffset(transaction.TargetDate.Value): null,
                FinishDate = transaction.FinishDate.HasValue? new DateTimeOffset(transaction.FinishDate.Value): null
            };
            var result = await this.commandDispatcher
                .DispatchAsync<CreateTransactionCommand, ServiceResult>(command);

            if (result.HasError)
            {
                this.logger.LogWarning(
                    "Error creating transaction : {Message}",
                    result.Error!.Message
                );

                //TODO: improve result to response convertion
                if (result.Error.Message.Contains("not found"))
                    return this.NotFound(result.Error);

                return this.BadRequest(result.Error);
            }

            this.logger.LogInformation("Finished creation of transaction");
            
            return this.Ok();
        }
    }
}
