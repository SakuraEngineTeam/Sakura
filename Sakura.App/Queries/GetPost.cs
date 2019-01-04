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
    protected readonly IConnectionFactory ConnectionFactory;

    public GetPostHandler(IConnectionFactory connectionFactory)
    {
      ConnectionFactory = connectionFactory;
    }

    public PostViewModel Handle(GetPost query)
    {
      try {
        var connection = ConnectionFactory.GetConnection();
        string sql = "SELECT post_id as id, message, created_at as createdAt FROM posts WHERE post_id = @Id";
        var resource = connection.QueryFirstOrDefault<PostViewModel>(sql, new {query.Id});
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
