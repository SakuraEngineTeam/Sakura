using System.Collections.Generic;
using Dapper;

namespace Sakura.App.Queries
{
  public class GetPosts : IQuery<IEnumerable<PostViewModel>> { }

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
        string sql = "SELECT post_id as id, view_id as viewId, message, created_at as createdAt FROM posts";
        return connection.Query<PostViewModel>(sql);
      }
      finally {
        ConnectionFactory.CloseConnection();
      }
    }
  }
}
