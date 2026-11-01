using Microsoft.Extensions.DependencyInjection;

using FinancialHub.CQRS.Results;
using FinancialHub.CQRS.Handlers.Commands;
using FinancialHub.CQRS.Handlers.Queries;

using FinancialHub.Transactions.Application.UseCases.Create;
using FinancialHub.Transactions.Application.UseCases.Delete;
using FinancialHub.Transactions.Application.UseCases.Get;
using FinancialHub.Transactions.Application.UseCases.Get.ById;
using FinancialHub.Transactions.Application.UseCases.Get.Categories;
using FinancialHub.Transactions.Application.UseCases.Get.Balances;
using FinancialHub.Transactions.Application.UseCases.Update;

namespace FinancialHub.Transactions.Application.UseCases
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
            services.AddScoped<IQueryHandler<GetTransactionQuery, GetTransactionResult>, GetTransactionQueryHandler>();
            services.AddScoped<IQueryHandler<GetTransactionByIdQuery, GetTransactionByIdResult>, GetTransactionByIdQueryHandler>();
            
            services.AddScoped<IQueryHandler<GetTransactionCategoriesQuery, GetTransactionCategoriesResult>, GetTransactionCategoriesQueryHandler>();
            services.AddScoped<IQueryHandler<GetTransactionBalancesQuery, GetTransactionBalancesResult>, GetTransactionBalancesQueryHandler>();

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateTransactionCommand, ServiceResult>, CreateTransactionCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateTransactionCommand, UpdateTransactionResult>, UpdateTransactionCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteTransactionCommand>, DeleteTransactionCommandHandler>();

            return services;
        }
    }
}
