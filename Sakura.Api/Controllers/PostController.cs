using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sakura.App.Queries;
using Sakura.Persistence;

namespace Sakura.Api.Controllers
{
  [ApiController]
  [Route("api/posts")]
  public class PostController : ControllerBase
  {
    protected readonly IQueryDispatcher QueryDispatcher;

    public PostController(IQueryDispatcher queryDispatcher)
    {
      QueryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public IEnumerable<PostViewModel> GetPosts()
    {
      var query = new GetPosts();
      return QueryDispatcher.Handle<GetPosts, IEnumerable<PostViewModel>>(query);
    }

    [HttpGet("{id}")]
    public ActionResult<PostViewModel> GetPost(Guid id)
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
