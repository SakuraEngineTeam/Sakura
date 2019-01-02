using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Sakura.App.Commands;
using Sakura.App.Queries;
using Sakura.Persistence;

namespace Sakura.Api.Controllers
{
  [ApiController]
  [Route("api/posts")]
  public class PostController : ControllerBase
  {
    protected readonly IDbConnection Connection;
    protected readonly Context Context;
    protected readonly PostRepository PostRepository;

    public PostController(IDbConnection connection, Context context, PostRepository postRepository)
    {
      Connection = connection;
      Context = context;
      PostRepository = postRepository;
    }

    [HttpPost]
    public ActionResult CreatePost([FromBody] CreatePost command)
    {
      var handler = new CreatePostHandler(Context, PostRepository);
      long id = handler.Handle(command);
      return Created($"api/posts/{id}", new {id});
    }

    [HttpGet]
    public IEnumerable<PostViewModel> GetPosts()
    {
      using (Connection) {
        var query = new GetPosts();
        var handler = new GetPostsHandler(Connection);
        return handler.Handle(query);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<PostViewModel> GetPost(long id)
    {
      using (Connection) {
        var query = new GetPost(id);
        var handler = new GetPostHandler(Connection);

        try {
          return handler.Handle(query);
        }
        catch (ModelNotFoundException) {
          return NotFound();
        }
      }
    }
  }
}
