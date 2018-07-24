using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TripAdvisor.Models;

namespace TripAdvisor.Controllers
{
  public class CitiesController : Controller
  {
    [HttpGet("/Cities/{countryId}")]
    public ActionResult Index(int countryId)
    {
      List<City> allCities = Country.Find(countryId).GetCities();
      return View(allCities);
    }
    [HttpGet("/Cities/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpGet("/Cities/{cityId}/attractions")]
    public ActionResult GetAttractions(int cityId)
    {
      return View("Attractions", City.Find(cityId).GetAttractions());
    }
  }
}
