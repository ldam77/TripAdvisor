using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TripAdvisor.Models;

namespace TripAdvisor.Controllers
{
  public class ActivitiesController : Controller
  {
    [HttpGet("/Activities")]
    public ActionResult Index()
    {
      List<Activity> allActivities = Activity.GetAll();
      return View(allActivities);
    }
    [HttpGet("/Activities/new")]
      public ActionResult CreateForm()
      {
        return View();
      }
  }
}
