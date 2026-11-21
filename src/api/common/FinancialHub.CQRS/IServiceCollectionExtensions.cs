using Microsoft.Extensions.DependencyInjection;
using FinancialHub.CQRS.Handlers.Commands;
using FinancialHub.CQRS.Handlers.Queries;

namespace FinancialHub.CQRS
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services)
        {
            return services
                .AddScoped<ICommandDispatcher, CommandDispatcher>()
                .AddScoped<IQueryDispatcher, QueryDispatcher>();
        }
    }
}
