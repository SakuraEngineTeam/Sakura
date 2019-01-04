using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sakura.App.Commands;
using Sakura.App.Queries;
using Sakura.Model;
using Sakura.Persistence;

namespace Sakura.Api.Controllers
{
  [ApiController]
  [Route("api/posts")]
  public class PostController : ControllerBase
  {
    protected readonly ICommandDispatcher CommandDispatcher;
    protected readonly IQueryDispatcher QueryDispatcher;

    public PostController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
      CommandDispatcher = commandDispatcher;
      QueryDispatcher = queryDispatcher;
    }

    [HttpPost]
    public ActionResult CreatePost([FromBody] CreatePost command)
    {
      try {
        long id = CommandDispatcher.Handle<CreatePost, long>(command);
        return Created($"api/posts/{id}", new {id});
      }
      catch (ValidationException e) {
        return BadRequest(new {error = e.Message});
      }
    }

    [HttpGet]
    public IEnumerable<PostViewModel> GetPosts()
    {
      var query = new GetPosts();
      return QueryDispatcher.Handle<GetPosts, IEnumerable<PostViewModel>>(query);
    }

    [HttpGet("{id}")]
    public ActionResult<PostViewModel> GetPost(long id)
    {
      try {
        var query = new GetPost(id);
        return QueryDispatcher.Handle<GetPost, PostViewModel>(query);
      }
      catch (ModelNotFoundException) {
        return NotFound();
      }
    }
  }
}
