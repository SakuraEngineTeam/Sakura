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
  t.thread_id as thread_id,
  t.bumped_at as bumped_at,
  p.post_id as id,
  p.view_id as view_id,
  p.message as message,
  p.created_at as created_at,
  r.post_id as reply_id,
  r.view_id as reply_view_id,
  r.message as reply_message,
  r.created_at as reply_created_at
FROM threads AS t
JOIN LATERAL (
  SELECT *
  FROM posts AS p
  WHERE p.thread_id = t.thread_id
  ORDER BY p.view_id
  LIMIT 1
) AS p ON p.thread_id = t.thread_id
JOIN LATERAL (
  SELECT *
  FROM posts AS p
  WHERE p.thread_id = t.thread_id
  ORDER BY p.view_id DESC
  LIMIT 3
) AS r ON r.thread_id = t.thread_id
ORDER BY t.bumped_at DESC, r.view_id";
        var threads = new Dictionary<Guid, ThreadPreviewViewModel>();
        foreach (dynamic row in connection.Query(sql)) {
          var post = new PostViewModel(
            row.thread_id,
            row.reply_id,
            row.reply_view_id,
            row.reply_message,
            row.reply_created_at);
          if (!threads.TryGetValue(post.ThreadId, out var thread)) {
            thread = new ThreadPreviewViewModel(
              post.ThreadId,
              row.id,
              row.view_id,
              row.message,
              row.created_at,
              row.bumped_at);
            threads.Add(post.ThreadId, thread);
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
