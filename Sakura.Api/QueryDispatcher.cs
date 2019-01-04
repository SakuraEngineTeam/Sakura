using System;
using Microsoft.Extensions.DependencyInjection;
using Sakura.App.Queries;

namespace Sakura.Api
{
  public class QueryDispatcher : IQueryDispatcher
  {
    protected readonly IServiceProvider ServiceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
      ServiceProvider = serviceProvider;
    }

    public TQueryResult Handle<TQuery, TQueryResult>(TQuery query) where TQuery : IQuery<TQueryResult>
    {
      var handler = ServiceProvider.GetService<IQueryHandler<TQuery, TQueryResult>>();

      if (handler == null) {
        string message = $"Can't resolve handler for {query.GetType()}";
        throw new ArgumentException(message, nameof(query));
      }

      return handler.Handle(query);
    }
  }
}
