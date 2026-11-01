using System.Text.Json.Serialization;
using FinancialHub.Transactions.Infra.Database;
using FinancialHub.Transactions.Application;

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
                options.SwaggerDoc("Transactions API v1", new() { Title = "My Financial Hub - Transactions API", Version = "v1" });
            });

            return services;
        }
    }
}
