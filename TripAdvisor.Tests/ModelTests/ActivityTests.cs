using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using TripAdvisor.Models;

namespace TripAdvisor.Tests
{
  [TestClass]
  public class activityTests : IDisposable
  {
    public void Dispose()
    {
      Food.DeleteAll();
      Activity.DeleteAll();

    }
    public activityTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=trip_advisor_test;";
    }
    // Get Name
    [TestMethod]
    public void GetNamefromActivity()
    {
      Activity testActivity = new Activity("park", "description");

      string testName = testActivity.GetName();

      Assert.AreEqual("park", testName);
    }
    //GetID
    [TestMethod]
    public void getIdFromActivity()
    {
      Activity testActivity = new Activity("park", "description");
      testActivity.Save();
      Activity savedActivity = Activity.GetAll()[0];
      int result = savedActivity.GetId();
      int testId = testActivity.GetId();

      Assert.AreEqual(testId, result);
    }
    //GetDescription
    [TestMethod]
    public void getDescriptionFromFood()
    {
      Activity testActivity = new Activity("park", "description");
      testActivity.Save();

      Activity savedActivity = Activity.GetAll()[0];

      string result = savedActivity.GetDescription();
      string testId = testActivity.GetDescription();

      Assert.AreEqual(testId, result);
    }

    // save
    [TestMethod]
    public void Save_SavesToDatabase_activity()
    {
      Activity testActivity = new Activity("park", "description");

      testActivity.Save();
      List<Activity> result = Activity.GetAll();
      List<Activity> testList = new List<Activity>{testActivity};
      CollectionAssert.AreEqual(testList, result);
    }

    //get all
    [TestMethod]
    public void GetAll_Activity()
    {
      Activity testActivity = new Activity("park", "description");

      testActivity.Save();
      List<Activity> result = Activity.GetAll();
      int resultNumber = Activity.GetAll().Count;

      Assert.AreEqual(1, resultNumber);
    }

    //Find
    [TestMethod]
    public void Find_FindActivityInDatabase()
    {
      Activity testActivity = new Activity("park", "description");
      testActivity.Save();

      Activity foundActivity = Activity.FindByActivityId(testActivity.GetId());

      Assert.AreEqual(testActivity.GetName(), foundActivity.GetName());
    }

    //Edit
    [TestMethod]
    public void Edit_EditFoodInDatabase()
    {
      Activity testActivity = new Activity("park", "description");
      testActivity.Save();
      testActivity.EditActivity("newpark", "description");

      Activity foundActivity = Activity.FindByActivityId(testActivity.GetId());
      Assert.AreEqual("newpark", foundActivity.GetName());
    }

    //Delete All
    [TestMethod]
    public void Delete_DeleteAllActivitiesInDatabase()
    {
      Activity testActivity = new Activity("park", "description");
      testActivity.Save();
      Activity.DeleteAll();
      List<Activity> result = Activity.GetAll();

      Assert.AreEqual(0, result.Count);
    }
  }
}
