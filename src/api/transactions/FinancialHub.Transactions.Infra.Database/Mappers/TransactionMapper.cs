using FinancialHub.Transactions.Domain;
using FinancialHub.Transactions.Domain.Data.Restore;
using FinancialHub.Transactions.Infra.Database.Entities;
using System;

namespace FinancialHub.Transactions.Infra.Database.Mappers
{
    internal sealed class TransactionMapper
    {
        private readonly TransactionCategoryMapper categoryMapper;
        private readonly TransactionBalanceMapper balanceMapper;

        public TransactionMapper(
            TransactionCategoryMapper categoryMapper,
            TransactionBalanceMapper balanceMapper
        ) 
        {
            this.categoryMapper = categoryMapper;
            this.balanceMapper = balanceMapper;
        }

        internal Transaction FromEntity(TransactionEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var balance = balanceMapper.FromEntity(entity.Balance);
            var category = categoryMapper.FromEntity(entity.Category);

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
                        Balance = balance,
                        Category = category
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

        public TransactionEntity? ToEntity(Transaction model)
        {
            var balance = balanceMapper.ToEntity(model.Balance);
            var category = categoryMapper.ToEntity(model.Category);
            if (balance is null || category is null)
                return null;

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
