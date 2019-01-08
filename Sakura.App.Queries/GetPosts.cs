using System;
using System.Collections.Generic;
using Dapper;

namespace Sakura.App.Queries
{
  public class GetPosts : IQuery<IEnumerable<PostViewModel>>
  {
    public readonly Guid? ThreadId;

    public GetPosts(Guid? threadId = null)
    {
      ThreadId = threadId;
    }
  }

  public class GetPostsHandler : IQueryHandler<GetPosts, IEnumerable<PostViewModel>>
  {
    protected readonly IConnectionFactory ConnectionFactory;

    public GetPostsHandler(IConnectionFactory connectionFactory)
    {
      ConnectionFactory = connectionFactory;
    }

    public IEnumerable<PostViewModel> Handle(GetPosts query)
    {
      try {
        var connection = ConnectionFactory.GetConnection();
        string sql = @"
SELECT
  thread_id as threadId,
  post_id as id,
  view_id as viewId,
  message,
  created_at as createdAt
FROM posts";

        if (query.ThreadId != null) {
          sql += " WHERE thread_id = @ThreadId";
        }

        sql += " ORDER BY created_at DESC";
        return connection.Query<PostViewModel>(sql, new {query.ThreadId});
      }
      finally {
        ConnectionFactory.CloseConnection();
      }
    }
  }
}
