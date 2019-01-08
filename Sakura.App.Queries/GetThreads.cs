using System.Collections.Generic;
using Dapper;

namespace Sakura.App.Queries
{
  public class GetThreads : IQuery<IEnumerable<ThreadViewModel>> { }

  public class GetThreadsHandler : IQueryHandler<GetThreads, IEnumerable<ThreadViewModel>>
  {
    protected readonly IConnectionFactory ConnectionFactory;

    public GetThreadsHandler(IConnectionFactory connectionFactory)
    {
      ConnectionFactory = connectionFactory;
    }

    public IEnumerable<ThreadViewModel> Handle(GetThreads query)
    {
      try {
        var connection = ConnectionFactory.GetConnection();
        string sql = @"
SELECT
  thread_id as threadId,
  bumped_at as bumpedAt
FROM threads
ORDER BY bumped_at DESC";
        return connection.Query<ThreadViewModel>(sql);
      }
      finally {
        ConnectionFactory.CloseConnection();
      }
    }
  }
}
