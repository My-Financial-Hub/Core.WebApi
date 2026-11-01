using System;

namespace FinancialHub.Transactions.Application.UseCases.Delete
{
    public sealed class DeleteTransactionCommand
    {
        public Guid TransactionId { get; }
        public DeleteTransactionCommand(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
