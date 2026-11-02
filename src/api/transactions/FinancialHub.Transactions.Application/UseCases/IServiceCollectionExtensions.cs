using Microsoft.Extensions.DependencyInjection;

using FinancialHub.CQRS.Results;
using FinancialHub.CQRS.Handlers.Commands;
using FinancialHub.CQRS.Handlers.Queries;

using FinancialHub.Transactions.Application.UseCases.Create;
using FinancialHub.Transactions.Application.UseCases.Get;
using FinancialHub.Transactions.Application.UseCases.Delete;
using FinancialHub.Transactions.Application.UseCases.Update;
using FinancialHub.Transactions.Application.UseCases.Get.ById;

namespace FinancialHub.Transactions.Application.UseCases
{
    internal static class IServiceCollectionExtensions
    {
        internal static IServiceCollection AddTransactionsUseCases(this IServiceCollection services)
        {
            services
                .AddTransactionsQueryUseCases()
                .AddTransactionsCommandUseCases();

            return services;
        }

        private static IServiceCollection AddTransactionsQueryUseCases(this IServiceCollection services)
        {
            services.AddScoped<IQueryHandler<GetTransactionQuery, GetTransactionResult>, GetTransactionQueryHandler>();
            services.AddScoped<IQueryHandler<GetTransactionByIdQuery, GetTransactionByIdResult>, GetTransactionByIdQueryHandler>();
            return services;
        }

        private static IServiceCollection AddTransactionsCommandUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateTransactionCommand, ServiceResult>, CreateTransactionCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateTransactionCommand, UpdateTransactionResult>, UpdateTransactionCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteTransactionCommand>, DeleteTransactionCommandHandler>();
            return services;
        }
    }
}
