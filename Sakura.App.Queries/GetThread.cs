using System;
using Dapper;
using Sakura.Persistence;

namespace Sakura.App.Queries
{
  public class GetThread : IQuery<ThreadViewModel>
  {
    public readonly Guid Id;

    public GetThread(Guid id)
    {
      Id = id;
    }
  }

  public class GetThreadHandler : IQueryHandler<GetThread, ThreadViewModel>
  {
    protected readonly IConnectionFactory ConnectionFactory;

    public GetThreadHandler(IConnectionFactory connectionFactory)
    {
      ConnectionFactory = connectionFactory;
    }

    public ThreadViewModel Handle(GetThread query)
    {
      try {
        var connection = ConnectionFactory.GetConnection();
        string sql = @"
SELECT
  thread_id as threadId,
  bumped_at as bumpedAt
FROM threads
WHERE thread_id = @Id
ORDER BY bumped_at DESC
LIMIT 1";
        var resource = connection.QueryFirstOrDefault<ThreadViewModel>(sql, new {query.Id});
        if (resource == null) {
          throw new ModelNotFoundException();
        }

        return resource;
      }
      finally {
        ConnectionFactory.CloseConnection();
      }
    }
  }
}
