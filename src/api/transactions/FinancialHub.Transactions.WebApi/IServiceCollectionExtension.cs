using System.Text.Json.Serialization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using FinancialHub.Transactions.Application;

using FinancialHub.Transactions.Infra.Database;

namespace FinancialHub.Transactions.WebApi
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTransactionsWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services
                .AddTransactionsDatabase(configuration)
                .AddTransactionsApplication();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "Transactions API v1", 
                    new() { 
                        Title = "My Financial Hub - Transactions API", 
                        Description = "API for managing financial transactions, including creation, retrieval, updating, and deletion of transactions, as well as handling transaction categories.",
                        Version = "v1"
                    }
                );
            });

            return services;
        }
    }
}
