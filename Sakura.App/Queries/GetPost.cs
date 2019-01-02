using System.Data;
using Dapper;
using Sakura.Persistence;

namespace Sakura.App.Queries
{
  public class GetPost : IQuery<PostViewModel>
  {
    public readonly long Id;

    public GetPost(long id)
    {
      Id = id;
    }
  }

  public class GetPostHandler : IQueryHandler<GetPost, PostViewModel>
  {
    protected readonly IDbConnection Connection;

    public GetPostHandler(IDbConnection connection)
    {
      Connection = connection;
    }

    public PostViewModel Handle(GetPost query)
    {
      string sql = "SELECT post_id as id, message, created_at as createdAt FROM posts WHERE post_id = @Id";
      var resource = Connection.QueryFirstOrDefault<PostViewModel>(sql, new { query.Id });
      if (resource == null) {
        throw new ModelNotFoundException();
      }

      return resource;
    }
  }
}
