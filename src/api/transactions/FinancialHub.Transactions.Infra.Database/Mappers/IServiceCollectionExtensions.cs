using Microsoft.Extensions.DependencyInjection;

namespace FinancialHub.Transactions.Infra.Database.Mappers
{
    internal static class IServiceCollectionExtensions
    {
        internal static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddScoped<TransactionCategoryMapper>();
            services.AddScoped<TransactionBalanceMapper>();
            services.AddScoped<TransactionMapper>();
            return services;
        }
    }
}
