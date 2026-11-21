using FinancialHub.Transactions.Domain.Entities;
using FinancialHub.Transactions.Infra.Database.Entities;
using System;

namespace FinancialHub.Transactions.Infra.Database.Mappers
{
    internal sealed class TransactionCategoryMapper
    {
        internal TransactionCategory FromEntity(TransactionCategoryEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new TransactionCategory(entity.Id, entity.Name);
        }

        internal TransactionCategoryEntity ToEntity(TransactionCategory model)
        {
            ArgumentNullException.ThrowIfNull(model);

            return new TransactionCategoryEntity(
                id: model.Id,
                name: model.Name,
                isActive: true
            );
        }
    }
}
