using System;

namespace FinancialHub.Transactions.Application.UseCases.Categories.Update
{
    public sealed class UpdateCategoryCommand
    {
        public Guid Id { get; }
        public string Name { get; }

        public UpdateCategoryCommand(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
