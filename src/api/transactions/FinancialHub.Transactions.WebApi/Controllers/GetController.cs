using Microsoft.AspNetCore.Mvc;
using FinancialHub.CQRS;
using FinancialHub.Transactions.Application.UseCases.Get;
using FinancialHub.Transactions.WebApi.Requests;

namespace FinancialHub.Transactions.WebApi.Controllers
{
    [Tags("Transactions")]
    public sealed class GetController : BaseController
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ILogger<GetController> logger;

        public GetController(IQueryDispatcher queryDispatcher, ILogger<GetController> logger)
        {
            this.queryDispatcher = queryDispatcher;
            this.logger = logger;
        }

        [HttpGet]
        //[EndpointSummary("Gets transactions by filters")]
        [ProducesResponseType(typeof(GetTransactionResult), 200)]
        public async Task<IActionResult> Get([FromQuery] GetTransactionsRequestQuery filters)
        {
            var query = new GetTransactionQuery(
                balances: filters.Balances,
                categories: filters.Categories,
                dateRange: 
                    filters.StartDate is not null && filters.EndDate is not null ?
                    (filters.StartDate.Value, filters.EndDate.Value) :
                    null,
                statuses: filters.Statuses,
                kinds: filters.Kinds
            );
            var result = await this.queryDispatcher.DispatchAsync<GetTransactionQuery, GetTransactionResult>(query);
            return Ok(result);
        }
    }
}
