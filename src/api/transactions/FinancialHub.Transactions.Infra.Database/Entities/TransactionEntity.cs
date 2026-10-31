using System;

namespace MyFinancialHub.Transactions.Infra.Database.Entities
{
    public class TransactionEntity
    {
        public Guid Id { get; private set; }

        public string Description { get; private set; }
        public decimal Amount { get; private set; }

        public DateTimeOffset? TargetDate { get; private set; }
        public DateTimeOffset? FinishDate { get; private set; }

        public Guid BalanceId { get; private set; }
        public TransactionBalanceEntity Balance { get; private set; }

        public Guid CategoryId { get; private set; }
        public TransactionCategoryEntity Category { get; private set; }

        public bool IsActive { get; private set;}
        public string Status { get; private set; }
        public string Type { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        
        private TransactionEntity() { }

        public TransactionEntity(
            Guid id, string description, decimal amount, 
            DateTimeOffset? targetDate, DateTimeOffset? finishDate,
            TransactionBalanceEntity balance, TransactionCategoryEntity category, 
            string status, string type, 
            DateTimeOffset createdAt, DateTimeOffset? updatedAt
        ): 
            this(id, description, amount, targetDate, finishDate, balance.Id, category.Id, status, type, createdAt, updatedAt)
        {
            this.Balance = balance;
            this.Category = category;
        }

        public TransactionEntity(
            Guid id,
            string description,
            decimal amount,
            DateTimeOffset? targetDate,
            DateTimeOffset? finishDate,
            Guid balanceId,
            Guid categoryId,
            string status,
            string type,
            DateTimeOffset createdAt,
            DateTimeOffset? updatedAt
        )
        {
            this.Id = id;

            this.Description = description;
            this.Amount = amount;
            
            this.TargetDate = targetDate;
            this.FinishDate = finishDate;
            
            this.BalanceId = balanceId;
            this.CategoryId = categoryId;
            
            this.Status = status;
            this.Type = type;
            
            this.IsActive = true;

            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
        }
    }
}
