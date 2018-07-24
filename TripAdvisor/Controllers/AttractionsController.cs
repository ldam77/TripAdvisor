using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TripAdvisor.Models;

namespace TripAdvisor.Controllers
{
  public class AttractionsController : Controller
  {
    [HttpGet("/Attractions")]
    public ActionResult Index()
    {
      List<Attraction> allAttractions = Attraction.GetAll();
      return View(allAttractions);
    }
    [HttpGet("/Attractions/new")]
      public ActionResult CreateForm()
      {
        return View();
      }
  }
}
