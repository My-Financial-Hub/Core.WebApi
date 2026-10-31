using System;
using System.Collections.Generic;

namespace MyFinancialHub.Transactions.Infra.Database.Entities
{
    public class TransactionBalanceEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<TransactionEntity> Transactions { get; private set; }

        public TransactionBalanceEntity(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.Transactions = new List<TransactionEntity>();
        }
    }
}
