using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;

using FinancialHub.CQRS;

using FinancialHub.Transactions.Application.UseCases.Get.ById;

namespace FinancialHub.Transactions.WebApi.Controllers
{
    [Tags("Transactions")]
    public sealed class GetByIdController : BaseTransactionsController
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ILogger<GetByIdController> logger;

        public GetByIdController(IQueryDispatcher queryDispatcher, ILogger<GetByIdController> logger)
        {
            this.queryDispatcher = queryDispatcher;
            this.logger = logger;
        }

        [HttpGet("{id:guid}")]
        //[EndpointSummary("Gets a transaction by its ID")]
        [ProducesResponseType(typeof(GetTransactionByIdResult), 200)]
        [ProducesResponseType(typeof(GetTransactionByIdResult), 404)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            this.logger.LogInformation("Starting searching for the transaction with ID {Id}", id);
            var query = new GetTransactionByIdQuery(id);

            var result = await queryDispatcher
                .DispatchAsync<GetTransactionByIdQuery, GetTransactionByIdResult>(query);
            if (result.HasError)
            {
                this.logger.LogWarning("Transaction with ID :{Id} not found", id);
                return NotFound(result);
            }

            this.logger.LogInformation("Finished transaction searching with ID {Id}", id);
            return Ok(result);
        }
    }
}
