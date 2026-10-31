using Microsoft.Extensions.DependencyInjection;
using MyFinancialHub.Transactions.Application.UseCases;

namespace FinancialHub.Transactions.Application
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTransactionsApplication(this IServiceCollection services)
        {
            return services.AddUseCases();
        }
    }
}
