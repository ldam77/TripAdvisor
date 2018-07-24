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
  }
}
