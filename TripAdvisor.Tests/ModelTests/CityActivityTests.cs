using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeBox.Models;
using System;
using System.Collections.Generic;

namespace TripAdvisor.Tests
{
  [TestClass]
  public class CityActivityTests : IDisposable
  {
    public void Dispose()
    {
      CityActivity.DeleteAll();
    }
    public CityActivityTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=trip_advisor_test;";
    }
    [TestMethod]
    public void GetTest_ReturnDataField()
    {
      // Arrange
      int testCityId = 1;
      int testActivityId = 1;
      CityActivity testCityActivity = new CityActivity(testCityId, testActivityId);

      // act
      int resultCityId = testCityActivity.GetCityID();
      int resultActivityId = testCityActivity.GetActivityID();

      // assert
      Assert.AreEqual(testCityId, resultCityId);
      Assert.AreEqual(testActivityId, resultActivityId);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfAllAreTheSame_CityActivity()
    {
      // Arrange, Act
      CityActivity firstCityActivity = new CityActivity(1, 1);
      CityActivity secondCityActivity = new CityActivity(1, 1);

      // Assert
      Assert.AreEqual(firstCityActivity, secondCityActivity);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      CityActivity testCityActivity = new CityActivity(1, 1);

      //Act
      testCityActivity.Save();
      CityActivity savedCityActivity = CityActivity.GetAll()[0];

      int result = savedCityActivity.GetId();
      int testId = testCityActivity.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void SaveAndGetAll_SavesToDatabaseAndReturnAll_CityActivity()
    {
      //Arrange
      CityActivity testCityActivity = new CityActivity(1, 1);

      //Act
      testCityActivity.Save();
      List<CityActivity> result = CityActivity.GetAll();
      List<CityActivity> testList = new List<CityActivity>{testCityActivity};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindCityActivityInDatabase_CityActivity()
    {
      //Arrange
      CityActivity testCityActivity = new CityActivity(1, 1);
      testCityActivity.Save();

      //Act
      CityActivity resultById = CityActivity.FindById(testCityActivity.GetId());

      //Assert
      Assert.AreEqual(testCityActivity, resultById);

    }
  }
}
