using System;
using Dapper;
using Sakura.Persistence;

namespace Sakura.App.Queries
{
  public class GetPost : IQuery<PostViewModel>
  {
    public readonly Guid Id;
    public readonly Guid? ThreadId;

    public GetPost(Guid id, Guid? threadId = null)
    {
      Id = id;
      ThreadId = threadId;
    }
  }

  public class GetPostHandler : IQueryHandler<GetPost, PostViewModel>
  {
    protected readonly IConnectionFactory ConnectionFactory;

    public GetPostHandler(IConnectionFactory connectionFactory)
    {
      ConnectionFactory = connectionFactory;
    }

    public PostViewModel Handle(GetPost query)
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
FROM posts
WHERE post_id = @Id";

        if (query.ThreadId != null) {
          sql += " AND thread_id = @ThreadId";
        }

        sql += " ORDER BY created_at DESC LIMIT 1";
        var resource = connection.QueryFirstOrDefault<PostViewModel>(sql, new {query.Id, query.ThreadId});
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
