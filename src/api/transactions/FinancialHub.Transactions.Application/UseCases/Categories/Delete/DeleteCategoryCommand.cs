using System;

namespace FinancialHub.Transactions.Application.UseCases.Categories.Delete
{
    public sealed class DeleteCategoryCommand
    {
        public Guid CategoryId { get; }

        public DeleteCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
