using System.Threading.Tasks;

namespace FinancialHub.CQRS
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) 
            where TCommand : class;
        Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command) 
            where TCommand : class 
            where TResult : class;
    }
}