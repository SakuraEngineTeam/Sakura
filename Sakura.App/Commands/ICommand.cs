namespace Sakura.App.Commands
{
  public interface ICommand<TResult> { }

  public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
  {
    TResult Handle(TCommand command);
  }
}
