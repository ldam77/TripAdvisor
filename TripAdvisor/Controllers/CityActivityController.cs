using Microsoft.AspNetCore.Mvc;

namespace TripAdvisor.Controllers
{
  public class CityActivityController : Controller
  {
    [HttpGet("/CityActivity")]
    public ActionResult Index()
    {
      List<CityActivity> newCityActivities = CityActivity.GetAll();
      return View(newCityActivities);
    }
  }
}
