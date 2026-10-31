using System.Threading.Tasks;

namespace FinancialHub.CQRS
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
            where TQuery : class
            where TResult : class;
    }
}
