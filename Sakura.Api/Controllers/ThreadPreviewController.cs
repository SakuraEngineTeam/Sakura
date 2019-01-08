using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sakura.App.Queries;

namespace Sakura.Api.Controllers
{
  [ApiController]
  [Route("api/thread-previews")]
  public class ThreadPreviewController : ControllerBase
  {
    protected readonly IQueryDispatcher QueryDispatcher;

    public ThreadPreviewController(
      IQueryDispatcher queryDispatcher)
    {
      QueryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public IEnumerable<ThreadPreviewViewModel> GetThreads()
    {
      var query = new GetThreadPreviews();
      return QueryDispatcher.Handle<GetThreadPreviews, IEnumerable<ThreadPreviewViewModel>>(query);
    }
  }
}
