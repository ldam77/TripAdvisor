using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TripAdvisor.Models;

namespace TripAdvisor.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
    [HttpGet("/Countries")]
    public ActionResult Countries()
    {
      List<Country> allCountries = Country.GetAll();
      return View(allCountries);
    }
    [HttpGet("/Search")]
    public ActionResult SearchForm()
    {
      return View();
    }
    [HttpPost("/Search")]
    public ActionResult Search(string searchType, string searchTerm)
    {
      if (searchType.Equals("forCity"))
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<City> allCities = City.FindByName(searchTerm);
        if (allCities.Count > 0){
          Country selectedCountry = Country.Find(allCities[0].GetCountryId());
          model.Add("selectedCountry", selectedCountry);
        }
        else
        {
          Country selectedCountry = new Country("");
          model.Add("selectedCountry", selectedCountry);
        }
        model.Add("allCities", allCities);
        return View("../Cities/Index", model);
      }
      else if (searchType.Equals("forAttraction"))
      {
        return View("../Attractions/Index", Attraction.FindByName(searchTerm));
      }
      else
      {
        return View("../Activities/Index", Activity.FindByActivityName(searchTerm));
      }
    }
    [HttpPost("/Countries/new")]
    public ActionResult Add(string countryName)
    {
      Country newCountry = new Country(countryName);
      newCountry.Save();
      return RedirectToAction("Countries");
    }
  }
}
