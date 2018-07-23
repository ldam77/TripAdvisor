using Microsoft.AspNetCore.Mvc;

namespace TripAdvisor.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
    [HttpGet("/countries")]
    public ActionResult Countries()
    {
      return View();
    }
  }
}
