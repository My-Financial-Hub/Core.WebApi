using System.Threading.Tasks;

namespace FinancialHub.CQRS.Handlers.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : class
    {
        Task Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, TResult> 
        where TCommand : class
        where TResult : class
    {
        Task<TResult> Handle(TCommand command);
    }
}
