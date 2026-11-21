using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialHub.CQRS.Handlers.Commands
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.Handle(command);
        }

        public async Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command)
            where TCommand : class
            where TResult : class
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return await handler.Handle(command);
        }
    }
}