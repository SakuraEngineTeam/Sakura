using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sakura.App.Commands;
using Sakura.App.Queries;
using Sakura.Model;
using Sakura.Persistence;

namespace Sakura.Api.Controllers
{
  [ApiController]
  [Route("api/threads")]
  public class ThreadController : ControllerBase
  {
    protected readonly ICommandDispatcher CommandDispatcher;
    protected readonly IQueryDispatcher QueryDispatcher;

    public ThreadController(
      ICommandDispatcher commandDispatcher,
      IQueryDispatcher queryDispatcher)
    {
      CommandDispatcher = commandDispatcher;
      QueryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public IEnumerable<ThreadViewModel> GetThreads()
    {
      var query = new GetThreads();
      return QueryDispatcher.Handle<GetThreads, IEnumerable<ThreadViewModel>>(query);
    }

    [HttpPost]
    public ActionResult CreateThread([FromBody] CreateThread command)
    {
      try {
        Guid id = CommandDispatcher.Handle<CreateThread, Guid>(command);
        return Created($"api/threads/{id}", new {id});
      }
      catch (ValidationException e) {
        return BadRequest(new {error = e.Message});
      }
    }

    [HttpGet("{id}")]
    public ActionResult<ThreadViewModel> GetThread(Guid id)
    {
      try {
        var query = new GetThread(id);
        return QueryDispatcher.Handle<GetThread, ThreadViewModel>(query);
      }
      catch (ModelNotFoundException) {
        return NotFound();
      }
    }

    [HttpGet("{id}/posts")]
    public IEnumerable<PostViewModel> GetPosts(Guid id)
    {
      var query = new GetPosts(id);
      return QueryDispatcher.Handle<GetPosts, IEnumerable<PostViewModel>>(query);
    }

    [HttpPost("{id}/posts")]
    public ActionResult CreatePost(Guid id, [FromBody] CreatePost command)
    {
      if (command.ThreadId != id) {
        return BadRequest(new {error = "Mismatch ID from URL and body"});
      }

      try {
        Guid postId = CommandDispatcher.Handle<CreatePost, Guid>(command);
        return Created($"api/threads/{id}/posts/{postId}", new {id = postId});
      }
      catch (ValidationException e) {
        return BadRequest(new {error = e.Message});
      }
    }

    [HttpGet("{id}/posts/{postId}")]
    public ActionResult<PostViewModel> GetPost(Guid id, Guid postId)
    {
      try {
        var query = new GetPost(postId, id);
        return QueryDispatcher.Handle<GetPost, PostViewModel>(query);
      }
      catch (ModelNotFoundException) {
        return NotFound();
      }
    }
  }
}
