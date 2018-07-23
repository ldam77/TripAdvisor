using Microsoft.AspNetCore.Mvc;

namespace TripAdvisor.Controllers
{
  public class ActivitiesController : Controller
  {
    [HttpGet("/Activities")]
    public ActionResult Index()
    {
      return View();
    }
    [HttpGet("/Activities/new")]
      public ActionResult CreateForm()
      {
        return View();
      }
  }
}
