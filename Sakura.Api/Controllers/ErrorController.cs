using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Sakura.Api.Controllers
{
  [ApiController]
  [Route("api/errors")]
  [Produces("application/json")]
  public class ErrorController : ControllerBase
  {
    [HttpGet("404")]
    public IActionResult ActionNotFound()
    {
      var error = new Dictionary<string, string>
      {
        {"error", "Action not found" }
      };

      return NotFound(error);
    }
  }
}
