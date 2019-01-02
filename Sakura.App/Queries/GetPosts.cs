using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Sakura.App.Queries
{
  public class GetPosts : IQuery<IEnumerable<PostViewModel>> { }

  public class GetPostsHandler : IQueryHandler<GetPosts, IEnumerable<PostViewModel>>
  {
    protected readonly IDbConnection Connection;

    public GetPostsHandler(IDbConnection connection)
    {
      Connection = connection;
    }

    public IEnumerable<PostViewModel> Handle(GetPosts query)
    {
      return Connection.Query<PostViewModel>("SELECT post_id as id, message, created_at as createdAt FROM posts");
    }
  }
}
