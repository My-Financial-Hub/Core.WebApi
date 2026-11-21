using System;
using System.Collections.Generic;

namespace FinancialHub.Transactions.Infra.Database.Entities
{
    public class TransactionCategoryEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public ICollection<TransactionEntity> Transactions { get; private set; }

        public TransactionCategoryEntity(Guid id, string name, bool isActive) 
        {
            this.Id = id;
            this.Name = name;
            this.IsActive = isActive;
            this.Transactions = new List<TransactionEntity>();
        }
    }
}
