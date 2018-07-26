using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TripAdvisor.Models;

namespace TripAdvisor.Controllers
{
  public class FoodsController : Controller
  {
    [HttpGet("/Foods")]
    public ActionResult Index()
    {
      List<Food> allFood = Food.GetAll();
      return View(allFood);
    }
    [HttpGet("/Foods/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpGet("/Foods/{id}/edit")]
    public ActionResult EditFoodDetails(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();

      Food selectedFood = Food.Find(id);
      List<City> cityMain = selectedFood.GetCitiesbyFoodId();
      City selectedCity = cityMain[0];
///
      // City selectedCity = City.Find(id);
      // List<Food> selectedFood = selectedCity.GetFoods();
      model.Add("Food", selectedFood);
      model.Add("City", selectedCity);
      return View(model);
    }

    [HttpPost("/Foods/{id}/edit/new")]
    public ActionResult EditFoodNameFinal(int id)
    {
      Food foundFood = Food.Find(id);
      foundFood.Edit(Request.Form["new-name"], Request.Form["new-description"]);

      Dictionary<string, object> model = new Dictionary<string, object>();

      List<City> cityMain = foundFood.GetCitiesbyFoodId();
      City selectedCity = cityMain[0];
      Food newNameFood = Food.Find(id);

      model.Add("Food", newNameFood);
      model.Add("City", selectedCity);

      return View("EditFoodDetails", model);
    }
  }
}
