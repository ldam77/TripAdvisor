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

      [HttpGet("/Activities/{id}/edit")]
      public ActionResult EditActivitiesDetails(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Activity selectedActivity = Activity.FindByActivityId(id);
        List<City> cityMain = selectedActivity.GetCitiesbyActivityId();
        City selectedCity = cityMain[0];

        model.Add("Activity", selectedActivity);
        model.Add("City", selectedCity);
        return View(model);
      }

      [HttpPost("/Activities/{id}/edit/new")]
      public ActionResult EditActivityNameFinal(int id)
      {
        Activity foundActivity = Activity.FindByActivityId(id);
        foundActivity.EditActivity(Request.Form["new-name"], Request.Form["new-description"]);

        Dictionary<string, object> model = new Dictionary<string, object>();

        List<City> cityMain = foundActivity.GetCitiesbyActivityId();
        City selectedCity = cityMain[0];
      //  List<Activity> selectedActivities = selectedCity.GetActivities();
        Activity selectedActivity = Activity.FindByActivityId(id);
        model.Add("Activity", selectedActivity);
        model.Add("City", selectedCity);

        return View("EditActivitiesDetails", model);
      }

  }
}
