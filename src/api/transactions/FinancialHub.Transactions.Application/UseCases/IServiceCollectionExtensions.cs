using Microsoft.Extensions.DependencyInjection;

namespace MyFinancialHub.Transactions.Application.UseCases
{
    internal static class IServiceCollectionExtensions
    {
        internal static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            return services
                .AddQueryHandlers()
                .AddCommandHandlers();
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            return services;
        }
    }
}
