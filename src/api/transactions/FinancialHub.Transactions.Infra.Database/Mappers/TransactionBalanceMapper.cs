using MyFinancialHub.Transactions.Infra.Database.Entities;

namespace MyFinancialHub.Transactions.Infra.Database.Mappers
{
    internal sealed class TransactionBalanceMapper
    {
        internal TransactionBalance FromEntity(TransactionBalanceEntity entity)
        {
            if (entity == null)
                return null;

            return new TransactionBalance(entity.Id, entity.Name);
        }

        internal TransactionBalanceEntity ToEntity(TransactionBalance model)
        {
            return new TransactionBalanceEntity(model.Id, model.Name);
        }
    }
}
