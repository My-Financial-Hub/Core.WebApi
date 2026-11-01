using Microsoft.Extensions.DependencyInjection;

using FinancialHub.CQRS;

using FinancialHub.Transactions.Application.UseCases;

namespace FinancialHub.Transactions.Application
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTransactionsApplication(this IServiceCollection services)
        {
            return services
                .AddCQRS()
                .AddUseCases();
        }
    }
}
