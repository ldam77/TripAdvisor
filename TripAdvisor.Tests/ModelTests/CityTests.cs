using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripAdvisor.Models;
using System;
using System.Collections.Generic;

namespace TripAdvisor.Tests
{
  [TestClass]
  public class CityTests : IDisposable
  {
    public void Dispose()
    {
      City.DeleteAll();
    }
    public CityTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=trip_advisor_test;";
    }
    [TestMethod]
    public void GetTest_ReturnDataField()
    {
      // Arrange
      int id = 1;
      string name = "testCity";
      int countryId = 1;
      City testCity = new City(name, countryId, id);

      // Act
      int resultId = testCity.GetId();
      string resultName = testCity.GetName();
      int resultCountryId = testCity.GetCountryId();

      // Assert
      Assert.AreEqual(id, resultId);
      Assert.AreEqual(name, resultName);
      Assert.AreEqual(countryId, resultCountryId);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfIdAndNameAreTheSame_City()
    {
      // Arrange, Act
      City firstCity = new City("testName", 1, 1);
      City secondCity = new City("testName", 1, 1);

      // Assert
      Assert.AreEqual(firstCity, secondCity);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      City testCity = new City("testName", 1);

      //Act
      testCity.Save();
      City savedCity = City.GetAll()[0];

      int result = savedCity.GetId();
      int testId = testCity.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void SaveAndGetAll_SavesToDatabaseAndReturnAll_City()
    {
      //Arrange
      City testCity = new City("testName", 1);

      //Act
      testCity.Save();
      List<City> result = City.GetAll();
      List<City> testList = new List<City>{testCity};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindCityInDatabase_City()
    {
      //Arrange
      City testCity = new City("testName", 1);
      testCity.Save();

      //Act
      City resultById = City.Find(testCity.GetId());

      //Assert
      Assert.AreEqual(testCity, resultById);
    }
    [TestMethod]
    public void GetAttractions_RetrievesAllAttractionsWithCityId_AttractionList()
    {
      // Arrange
      City testCity = new City("testCity", 1);
      testCity.Save();
      Attraction testAttraction = new Attraction("testAttraction", testCity.GetId());
      testAttraction.Save();
      List<Attraction> testAttractions = new List<Attraction> {testAttraction};

      // Act
      List<Attraction> resultAttractions = testCity.GetAttractions();

      // Assert
      CollectionAssert.AreEqual(testAttractions, resultAttractions);
    }
  }
}
