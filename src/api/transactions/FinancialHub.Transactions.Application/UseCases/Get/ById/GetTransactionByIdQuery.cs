using System;

namespace FinancialHub.Transactions.Application.UseCases.Get.ById
{
    public sealed class GetTransactionByIdQuery
    {
        public Guid TransactionId { get; }

        public GetTransactionByIdQuery(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
