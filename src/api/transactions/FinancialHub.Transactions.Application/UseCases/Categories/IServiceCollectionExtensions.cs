using Microsoft.Extensions.DependencyInjection;

using FinancialHub.CQRS.Handlers.Commands;
using FinancialHub.CQRS.Handlers.Queries;

using FinancialHub.Transactions.Application.UseCases.Categories.Get;
using FinancialHub.Transactions.Application.UseCases.Categories.Create;
using FinancialHub.Transactions.Application.UseCases.Categories.Delete;

namespace FinancialHub.Transactions.Application.UseCases.Categories
{
    internal static class IServiceCollectionExtensions
    {
        internal static IServiceCollection AddCategoriesUseCases(this IServiceCollection services)
        {
            return services
                .AddCategoriesQueryUseCases()
                .AddCategoriesCommandUseCases();
        }

        private static IServiceCollection AddCategoriesQueryUseCases(this IServiceCollection services)
        {
            services.AddScoped<IQueryHandler<GetTransactionCategoriesQuery, GetTransactionCategoriesResult>, GetTransactionCategoriesQueryHandler>();

            return services;
        }

        private static IServiceCollection AddCategoriesCommandUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateCategoryCommand, CreateCategoryResult>, CreateCategoryCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteCategoryCommand>, DeleteCategoryCommandHandler>();
            
            return services;
        }
    }
}
