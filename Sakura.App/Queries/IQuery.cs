namespace Sakura.App.Queries
{
  public interface IQuery<TResult> { }

  public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
  {
    TResult Handle(TQuery query);
  }

  public interface IQueryDispatcher
  {
    TQueryResult Handle<TQuery, TQueryResult>(TQuery query) where TQuery : IQuery<TQueryResult>;
  }
}
