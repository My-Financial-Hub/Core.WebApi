using Microsoft.Extensions.DependencyInjection;

namespace MyFinancialHub.Transactions.Infra.Database
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTransactionsDatabase(this IServiceCollection services)
        {
            return services;
        }
    }
}
