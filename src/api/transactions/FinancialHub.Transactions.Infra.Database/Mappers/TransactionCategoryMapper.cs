using MyFinancialHub.Transactions.Infra.Database.Entities;

namespace MyFinancialHub.Transactions.Infra.Database.Mappers
{
    internal sealed class TransactionCategoryMapper
    {
        internal TransactionCategory FromEntity(TransactionCategoryEntity entity)
        {
            if (entity == null)
                return null;

            return new TransactionCategory(entity.Id, entity.Name);
        }

        internal TransactionCategoryEntity ToEntity(TransactionCategory model)
        {
            return new TransactionCategoryEntity(
                id: model.Id,
                name: model.Name,
                isActive: true
            );
        }
    }
}
