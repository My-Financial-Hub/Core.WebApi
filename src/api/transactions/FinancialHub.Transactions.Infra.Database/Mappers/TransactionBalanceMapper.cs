using FinancialHub.Transactions.Domain.Entities;
using FinancialHub.Transactions.Infra.Database.Entities;
using System;

namespace FinancialHub.Transactions.Infra.Database.Mappers
{
    internal sealed class TransactionBalanceMapper
    {
        internal TransactionBalance FromEntity(TransactionBalanceEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new TransactionBalance(entity.Id, entity.Name);
        }

        internal TransactionBalanceEntity ToEntity(TransactionBalance model)
        {
            ArgumentNullException.ThrowIfNull(model);

            return new TransactionBalanceEntity(model.Id, model.Name);
        }
    }
}
