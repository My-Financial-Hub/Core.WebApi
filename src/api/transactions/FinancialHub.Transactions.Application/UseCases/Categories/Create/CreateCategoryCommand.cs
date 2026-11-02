namespace FinancialHub.Transactions.Application.UseCases.Categories.Create
{
    public sealed class CreateCategoryCommand
    {
        public string Name { get; }

        public CreateCategoryCommand(string name)
        {
            Name = name;
        }
    }
}
