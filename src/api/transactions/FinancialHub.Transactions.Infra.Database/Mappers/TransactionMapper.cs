using MyFinancialHub.Transactions.Infra.Database.Entities;

namespace MyFinancialHub.Transactions.Infra.Database.Mappers
{
    internal sealed class TransactionMapper
    {
        private readonly TransactionCategoryMapper categoryMapper;
        private readonly TransactionBalanceMapper balanceMapper;

        internal TransactionMapper(
            TransactionCategoryMapper categoryMapper,
            TransactionBalanceMapper balanceMapper
        ) 
        {
            this.categoryMapper = categoryMapper;
            this.balanceMapper = balanceMapper;
        }

        internal Transaction FromEntity(TransactionEntity entity)
        {
            return Transaction.Restore(
                new TransactionRestoreData()
                {
                    Id = entity.Id,
                    Dates = new TransactionRestoreDates()
                    {
                        TargetDate = entity.TargetDate,
                        FinishDate = entity.FinishDate,

                        CreatedAt = entity.CreatedAt,
                        UpdatedAt = entity.UpdatedAt
                    },
                    Relations = new TransactionRestoreRelations()
                    {
                        Balance = balanceMapper.FromEntity(entity.Balance),
                        Category = categoryMapper.FromEntity(entity.Category)
                    },
                    Details = new TransactionRestoreDetails()
                    {
                        Description = entity.Description,
                        Amount = entity.Amount,
                        Kind = entity.Type,
                        Status = entity.Status
                    }
                }
            );
        }

        public TransactionEntity ToEntity(Transaction model)
        {
            var balance = balanceMapper.ToEntity(model.Balance);
            var category = categoryMapper.ToEntity(model.Category);

            return new TransactionEntity(
                id: model.Id, 
                description: string.Empty,
                amount: model.Amount.Value,
                targetDate: model.TargetDate,
                finishDate: model.FinishDate,
                balance: balance,
                category: category,
                status: model.Status.ToString(),
                type: model.Kind.ToString(),
                createdAt: model.CreatedAt,
                updatedAt: model.UpdatedAt
            );
        }
    }
}
