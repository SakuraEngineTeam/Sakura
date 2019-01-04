using System;
using Microsoft.Extensions.DependencyInjection;
using Sakura.App.Commands;

namespace Sakura.Api
{
  public class CommandDispatcher : ICommandDispatcher
  {
    protected readonly IServiceProvider ServiceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
      ServiceProvider = serviceProvider;
    }

    public TCommandResult Handle<TCommand, TCommandResult>(TCommand command) where TCommand : ICommand<TCommandResult>
    {
      var handler = ServiceProvider.GetService<ICommandHandler<TCommand, TCommandResult>>();

      if (handler == null) {
        string message = $"Can't resolve handler for {command.GetType()}";
        throw new ArgumentException(message, nameof(command));
      }

      return handler.Handle(command);
    }
  }
}
