using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TripAdvisor.Models;

namespace TripAdvisor.Controllers
{
  public class CitiesController : Controller
  {
    [HttpGet("/Cities/Country={countryId}")]
    public ActionResult Index(int countryId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Country selectedCountry = Country.Find(countryId);
      List<City> allCities = selectedCountry.GetCities();
      model.Add("selectedCountry", selectedCountry);
      model.Add("allCities", allCities);
      return View(model);
    }


    [HttpGet("/Cities/{cityId}")]
    public ActionResult Detail(int cityId)
    {
      return View(City.Find(cityId));
    }

    [HttpPost("/Cities/{countryId}/Add")]
    public ActionResult Add(int countryId, string cityName)
    {
      City newCity = new City(cityName, countryId);
      newCity.Save();
      return RedirectToAction("Index", new { id = countryId});
    }
    [HttpPost("/Cities/{cityId}/AddAttraction")]
    public ActionResult AddAttraction(string attractionName, int cityId)
    {
      Attraction newAttraction = new Attraction(attractionName, cityId, "No description");
      newAttraction.Save();
      return RedirectToAction("Detail", new {id = cityId});
    }
    [HttpPost("/Cities/{cityId}/AddActivity")]
    public ActionResult AddActivity(string activityName, int cityId)
    {
      if(Activity.FindByActivityName(activityName).Count == 0)
      {
        Activity newActivity = new Activity(activityName, "");
        newActivity.Save();
        CityActivity newPair = new CityActivity(cityId, newActivity.GetId());
        newPair.Save();
      }
      else
      {
        CityActivity newPair = new CityActivity(cityId, Activity.FindByActivityName(activityName)[0].GetId());
        newPair.Save();
      }
      return RedirectToAction("Detail", new {id = cityId});
    }
    [HttpPost("/Cities/{cityId}/AddFood")]
    public ActionResult AddFood(string foodName, int cityId)
    {
      if(Food.FindByName(foodName).Count == 0)
      {
        Food newFood = new Food(foodName, "");
        newFood.Save();
        CityFood newPair = new CityFood(cityId, newFood.GetId());
        newPair.Save();
      }
      else
      {
        CityFood newPair = new CityFood(cityId, Food.FindByName(foodName)[0].GetId());
        newPair.Save();
      }
      return RedirectToAction("Detail", new {id = cityId});
    }

    // [HttpGet("/Cities/{cityId}/attractions")]
    // public ActionResult GetAttractions(int cityId)
    // {
    //   return View("Attractions", City.Find(cityId).GetAttractions());
    // }
  }
}
