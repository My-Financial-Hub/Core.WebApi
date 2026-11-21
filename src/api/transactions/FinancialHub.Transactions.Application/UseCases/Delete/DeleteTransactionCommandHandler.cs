using Microsoft.Extensions.Logging;
using FinancialHub.CQRS.Handlers.Commands;
using FinancialHub.Transactions.Domain.Interfaces;
using System.Threading.Tasks;

namespace FinancialHub.Transactions.Application.UseCases.Delete
{
    internal sealed class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ILogger<DeleteTransactionCommandHandler> logger;

        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository, ILogger<DeleteTransactionCommandHandler> logger)
        {
            this.transactionRepository = transactionRepository;
            this.logger = logger;
            
        }

        public async Task Handle(DeleteTransactionCommand command)
        {
            logger.LogInformation("Handling DeleteTransactionCommand for Transaction ID {TransactionId}.", command.TransactionId);

            var transaction = await transactionRepository.GetByIdAsync(command.TransactionId);

            if (transaction == null)
            {
                logger.LogWarning("Transaction with ID {TransactionId} not found for deletion.", command.TransactionId);
            }
            else
            {
                await transactionRepository.DeleteAsync(command.TransactionId);
                logger.LogInformation("Transaction with ID {TransactionId} deleted successfully.", command.TransactionId);
            }
        }
    }
}
