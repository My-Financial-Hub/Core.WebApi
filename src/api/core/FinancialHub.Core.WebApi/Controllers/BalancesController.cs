using FinancialHub.Core.Domain.DTOS.Balances;
using Microsoft.Extensions.Logging;

namespace FinancialHub.Core.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesErrorResponseType(typeof(Exception))]
    public sealed class BalancesController : BaseController
    {
        private readonly IBalancesService service;
        private readonly ILogger<BalancesController> logger;

        public BalancesController(IBalancesService service, ILogger<BalancesController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpGet]
        [Obsolete("Not implemented yet, use /accounts/{accountId}/balances instead")]
        public async Task<IActionResult> GetAll()
        {
            this.logger.LogInformation("Getting all balances");
            this.logger.LogInformation("Succesfully returned all balances");
            return ListResponse(Array.Empty<BalanceDto>());
        }

        /// <summary>
        /// Gets a balance by its ID.
        /// </summary>
        /// <param name="id">The ID of the balance to retrieve.</param>
        /// <returns>A response containing the balance data or an error message.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BalanceDto), 200)]
        [ProducesResponseType(typeof(NotFoundErrorResponse), 404)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            this.logger.LogInformation("Getting balance by id");
            var result = await service.GetByIdAsync(id);

            if (result.HasError)
            {
                this.logger.LogWarning(
                    "Error getting balance {BalanceId} : {Message}",
                    id, result.Error.Message
                );
                return ErrorResponse(result.Error);
            }

            this.logger.LogInformation("Succesfully returned balance by id");
            return Ok(result.Data);
        }

        /// <summary>
        /// Creates a new balance.
        /// </summary>
        /// <param name="balance">The balance to be created.</param>
        /// <returns>A response indicating the result of the creation operation.</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ValidationsErrorResponse), 400)]
        public async Task<IActionResult> Create([FromBody] CreateBalanceDto balance)
        {
            this.logger.LogInformation("Starting creation of balance");
            var result = await this.service.CreateAsync(balance);

            if (result.HasError)
            {
                this.logger.LogWarning(
                    "Error creating balance : {Message}",
                    result.Error.Message
                );
                return ErrorResponse(result.Error);
            }

            this.logger.LogInformation("Finished creation of Balance");
            return Created($"balances/{result.Data.Id}", result.Data);
        }

        /// <summary>
        /// Updates an existing balance with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the balance to be updated.</param>
        /// <param name="balance">The updated balance data.</param>
        /// <returns>A response indicating the result of the update operation.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(NotFoundErrorResponse), 404)]
        [ProducesResponseType(typeof(ValidationsErrorResponse), 400)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBalanceDto balance)
        {
            this.logger.LogInformation("Updating balance");
            var result = await this.service.UpdateAsync(id, balance);

            if (result.HasError)
            {
                this.logger.LogWarning(
                    "Error updating balance : {Message}",
                    result.Error.Message
                );
                return ErrorResponse(result.Error);
            }

            this.logger.LogInformation("Balance updated");
            return Ok();
        }

        /// <summary>
        /// Deletes an existing balance with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the balance to be deleted.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            this.logger.LogInformation("Removing balance");
            await service.DeleteAsync(id);
            this.logger.LogInformation("Balance removed");

            return NoContent();
        }
    }
}
