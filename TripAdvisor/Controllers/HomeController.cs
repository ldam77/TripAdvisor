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
  }
}
