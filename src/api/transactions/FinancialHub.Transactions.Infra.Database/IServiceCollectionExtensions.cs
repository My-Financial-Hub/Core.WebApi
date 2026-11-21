using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using FinancialHub.Transactions.Domain.Interfaces;

using FinancialHub.Transactions.Infra.Database.Context;
using FinancialHub.Transactions.Infra.Database.Repositories;
using FinancialHub.Transactions.Infra.Database.Mappers;

namespace FinancialHub.Transactions.Infra.Database
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTransactionsDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TransactionsDbContext>(
                provider =>
                    provider.UseSqlServer(
                        configuration.GetConnectionString("Transactions"),
                        x => x.MigrationsHistoryTable("transactions_migrations")
                    )
            );

            services
                .AddMappers()
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionsRepository>();
            services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
            services.AddScoped<ITransactionBalanceRepository, TransactionBalanceRepository>();
            return services;
        }
    }
}
