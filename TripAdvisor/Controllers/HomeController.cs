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
        return View("SearchResult", City.FindByName(searchTerm));
      }
      else if (searchType.Equals("forAttraction"))
      {
        return View("SearchResult", Attraction.FindByName(searchTerm));
      }
      else
      {
        return View("SearchResult", Activity.FindByActivityName(searchTerm));
      }
    }
  }
}
