using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialHub.CQRS.Handlers.Queries
{
    internal class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
            where TQuery : class
            where TResult : class
        {
            var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await handler.Handle(query);
        }
    }
}
