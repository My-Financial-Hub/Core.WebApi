using Microsoft.Extensions.Logging;
using FinancialHub.CQRS.Handlers.Queries;
using FinancialHub.Transactions.Domain.Interfaces;
using System.Threading.Tasks;

namespace FinancialHub.Transactions.Application.UseCases.Get.Balances
{
    internal sealed class GetTransactionBalancesQueryHandler : IQueryHandler<GetTransactionBalancesQuery, GetTransactionBalancesResult>
    {
        private readonly ITransactionBalanceRepository repository;
        private readonly ILogger<GetTransactionBalancesQueryHandler> logger;

        public GetTransactionBalancesQueryHandler(ITransactionBalanceRepository repository, ILogger<GetTransactionBalancesQueryHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<GetTransactionBalancesResult> Handle(GetTransactionBalancesQuery query)
        {
            var balances = await repository.GetAllAsync();
            return new GetTransactionBalancesResult(balances);
        }
    }
}
