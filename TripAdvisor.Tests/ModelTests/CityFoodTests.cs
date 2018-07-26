using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripAdvisor.Models;
using System;
using System.Collections.Generic;

namespace TripAdvisor.Tests
{
  [TestClass]
  public class CityFoodTests : IDisposable
  {
    public void Dispose()
    {
      CityFood.DeleteAll();
    }
    public CityFoodTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=trip_advisor_test;";
    }
    [TestMethod]
    public void GetTest_ReturnDataField()
    {
      // Arrange
      int testCityId = 1;
      int testFoodId = 1;
      CityFood testCityFood = new CityFood(testCityId, testFoodId);

      // act
      int resultCityId = testCityFood.GetCityID();
      int resultFoodId = testCityFood.GetFoodID();

      // assert
      Assert.AreEqual(testCityId, resultCityId);
      Assert.AreEqual(testFoodId, resultFoodId);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfAllAreTheSame_CityFood()
    {
      // Arrange, Act
      CityFood firstCityFood = new CityFood(1, 1);
      CityFood secondCityFood = new CityFood(1, 1);

      // Assert
      Assert.AreEqual(firstCityFood, secondCityFood);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      CityFood testCityFood = new CityFood(1, 1);

      //Act
      testCityFood.Save();
      CityFood savedCityFood = CityFood.GetAll()[0];

      int result = savedCityFood.GetId();
      int testId = testCityFood.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void SaveAndGetAll_SavesToDatabaseAndReturnAll_CityFood()
    {
      //Arrange
      CityFood testCityFood = new CityFood(1, 1);

      //Act
      testCityFood.Save();
      List<CityFood> result = CityFood.GetAll();
      List<CityFood> testList = new List<CityFood>{testCityFood};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindCityFoodInDatabase_CityFood()
    {
      //Arrange
      CityFood testCityFood = new CityFood(1, 1);
      testCityFood.Save();

      //Act
      CityFood resultById = CityFood.FindById(testCityFood.GetId());

      //Assert
      Assert.AreEqual(testCityFood, resultById);

    }
  }
}
