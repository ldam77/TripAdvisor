using Microsoft.AspNetCore.Mvc;

namespace TripAdvisor.Controllers
{
  public class CityFoodController : Controller
  {
    [HttpGet("/CityFood")]
    public ActionResult Index()
    {
      List<CityFood> newCityFood = CityFood.GetAll();
      return View(newCityFood);
    }
  }
}
