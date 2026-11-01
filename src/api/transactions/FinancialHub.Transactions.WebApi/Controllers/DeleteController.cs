using Microsoft.AspNetCore.Mvc;
using FinancialHub.CQRS;
using FinancialHub.Transactions.Application.UseCases.Delete;

namespace FinancialHub.Transactions.WebApi.Controllers
{
    [Tags("Transactions")]
    public sealed class DeleteController : BaseController
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
            var command = new DeleteTransactionCommand(id);
            await commandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
