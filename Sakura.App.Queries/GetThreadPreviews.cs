using System;
using System.Collections.Generic;
using Dapper;

namespace Sakura.App.Queries
{
  public class GetThreadPreviews : IQuery<IEnumerable<ThreadPreviewViewModel>> { }

  public class GetThreadPreviewsHandler : IQueryHandler<GetThreadPreviews, IEnumerable<ThreadPreviewViewModel>>
  {
    protected readonly IConnectionFactory ConnectionFactory;

    public GetThreadPreviewsHandler(IConnectionFactory connectionFactory)
    {
      ConnectionFactory = connectionFactory;
    }

    public IEnumerable<ThreadPreviewViewModel> Handle(GetThreadPreviews query)
    {
      try {
        var connection = ConnectionFactory.GetConnection();
        string sql = @"
SELECT
  t.thread_id as threadId,
  t.bumped_at as bumpedAt,
  p.post_id as id,
  p.view_id as viewId,
  p.message as message,
  p.created_at as createdAt
FROM threads AS t
JOIN LATERAL (
  SELECT *
  FROM posts AS p
  WHERE p.thread_id = t.thread_id
  ORDER BY p.view_id DESC
  LIMIT 3
) p ON p.thread_id = t.thread_id
ORDER  BY t.bumped_at DESC, p.view_id";
        var threads = new Dictionary<Guid, ThreadPreviewViewModel>();
        foreach (dynamic row in connection.Query(sql)) {
          var post = new PostViewModel(row.threadid, row.id, row.viewid, row.message, row.createdat);
          if (!threads.TryGetValue(post.ThreadId, out var thread)) {
            threads.Add(post.ThreadId, thread = new ThreadPreviewViewModel(post.ThreadId, row.bumpedat));
          }

          thread.Posts.Add(post);
        }

        return threads.Values;
      }
      finally {
        ConnectionFactory.CloseConnection();
      }
    }
  }
}
