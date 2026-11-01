using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using FinancialHub.CQRS.Handlers.Queries;

using FinancialHub.Transactions.Domain.Interfaces;

namespace FinancialHub.Transactions.Application.UseCases.Get
{
    internal sealed class GetTransactionQueryHandler : IQueryHandler<GetTransactionQuery, GetTransactionResult>
    {
        private readonly ITransactionRepository repository;
        private readonly ILogger<GetTransactionQueryHandler> logger;

        public GetTransactionQueryHandler(ITransactionRepository repository, ILogger<GetTransactionQueryHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<GetTransactionResult> Handle(GetTransactionQuery query)
        {
            var filters = new TransactionFilter(
                balances: query.Balances,
                categories: query.Categories,
                dateRange: query.DateRange,
                statuses: query.Statuses?.Select(status => status.ToString())?.ToArray(),
                kinds: query.Kinds?.Select(kind => kind.ToString())?.ToArray()
            );
            var transactions = await this.repository.GetAsync(filters);

            return new GetTransactionResult(transactions);
        }
    }
}
