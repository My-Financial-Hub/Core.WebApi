using Microsoft.Extensions.DependencyInjection;

namespace FinancialHub.Transactions.Infra.Database.Mappers
{
    internal static class IServiceCollectionExtensions
    {
        internal static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddSingleton<TransactionCategoryMapper>();
            services.AddSingleton<TransactionBalanceMapper>();
            services.AddSingleton<TransactionMapper>();
            return services;
        }
    }
}
