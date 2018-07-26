using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using TripAdvisor.Models;

namespace TripAdvisor.Tests
{
  [TestClass]
  public class foodTests : IDisposable
  {
    public void Dispose()
    {
      Food.DeleteAll();
    }
    public foodTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=trip_advisor_test;";
    }
    // Get Name
    [TestMethod]
    public void GetNamefromFood()
    {
      Food testFood = new Food("food", "description");
      testFood.Save();

      Food savedFood = Food.GetAll()[0];

      string resultName = savedFood.GetName();
      string testName = testFood.GetName();

      Assert.AreEqual(testName, resultName);
    }

    [TestMethod]
    public void getIdFromFood()
    {
      Food testFood = new Food("food", "descriptions");
      testFood.Save();

      Food savedFood = Food.GetAll()[0];

      int result = savedFood.GetId();
      int testId = testFood.GetId();

      Assert.AreEqual(testId, result);
    }
//GetDescription
    [TestMethod]
    public void getDescriptionFromFood()
    {
      Food testFood = new Food("food", "description");
      testFood.Save();

      Food savedFood = Food.GetAll()[0];

      string result = savedFood.GetDescription();
      string testDescription = testFood.GetDescription();

      Assert.AreEqual(testDescription, result);
    }



    // save
    [TestMethod]
    public void Save_SavesToDatabase_food()
    {
      Food testFood = new Food("food", "description");

      testFood.Save();
      List<Food> result = Food.GetAll();
      List<Food> testList = new List<Food>{testFood};
      CollectionAssert.AreEqual(testList, result);
    }

    //get all
    [TestMethod]
    public void GetAll_Food()
    {
      Food testFood = new Food("food", "description");

      testFood.Save();
      List<Food> result = Food.GetAll();
      int resultNumber = Food.GetAll().Count;

      Assert.AreEqual(1, resultNumber);
    }

    //Find
    [TestMethod]
    public void Find_FindFoodInDatabase()
    {
      Food testFood = new Food("food", "description");
      testFood.Save();

      Food foundFood = Food.Find(testFood.GetId());

      Assert.AreEqual(testFood.GetName(), foundFood.GetName());
    }

    //Find
    [TestMethod]
    public void Edit_EditFoodInDatabase()
    {
      Food testFood = new Food("food", "description");
      testFood.Save();
      testFood.Edit("newfood", "newdescription");
      Food foundFood = Food.Find(testFood.GetId());

      Assert.AreEqual("newfood", foundFood.GetName());
    }

    //Delete All
    [TestMethod]
    public void Delete_DeleteAllActivitiesInDatabase()
    {
      Food testFood = new Food("food", "description");
      testFood.Save();
      Food.DeleteAll();
      List<Food> result = Food.GetAll();

      Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
public void GetCitiesByfoodId()
{
  City testCity1 = new City("testName", 1);
  testCity1.Save();
  Food testFood1 = new Food("test1", "testDescription");
  testFood1.Save();

  CityFood newPair = new CityFood(testCity1.GetId(), testFood1.GetId());
  newPair.Save();

  List<City> testCity = testFood1.GetCitiesbyFoodId();
  Assert.AreEqual(testCity1.GetId(), testCity[0].GetId());
}
  }
}
