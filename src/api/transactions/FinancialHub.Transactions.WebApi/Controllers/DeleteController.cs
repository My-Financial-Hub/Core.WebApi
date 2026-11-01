using Microsoft.AspNetCore.Mvc;
using FinancialHub.CQRS;
using FinancialHub.Transactions.Application.UseCases.Delete;

namespace FinancialHub.Transactions.WebApi.Controllers
{
    [Tags("Transactions")]
    public sealed class DeleteController(ICommandDispatcher commandDispatcher, ILogger<DeleteController> logger) : BaseController
    {
        private readonly ICommandDispatcher commandDispatcher = commandDispatcher;
        private readonly ILogger<DeleteController> logger = logger;

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new DeleteTransactionCommand(id);
            await commandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
