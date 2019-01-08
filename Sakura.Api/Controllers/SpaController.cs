using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Sakura.Api.Controllers
{
  public class SpaController : Controller
  {
    protected readonly IHostingEnvironment Environment;

    public SpaController(IHostingEnvironment environment)
    {
      Environment = environment;
    }

    public IActionResult Index()
    {
      string path = Path.Combine(Environment.WebRootPath, "index.html");
      return new PhysicalFileResult(path, "text/html");
    }
  }
}
