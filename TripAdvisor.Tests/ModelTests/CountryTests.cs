using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripAdvisor.Models;
using System;
using System.Collections.Generic;

namespace TripAdvisor.Tests
{
  [TestClass]
  public class CountryTests : IDisposable
  {
    public void Dispose()
    {
      Country.DeleteAll();
    }
    public CountryTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=trip_advisor_test;";
    }
    [TestMethod]
    public void GetTest_ReturnDataField()
    {
      // Arrange
      int id = 1;
      string name = "testCountry";
      Country testCountry = new Country(name, id);

      // Act
      int resultId = testCountry.GetId();
      string resultName = testCountry.GetName();

      // Assert
      Assert.AreEqual(id, resultId);
      Assert.AreEqual(name, resultName);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfIdAndNameAreTheSame_Country()
    {
      // Arrange, Act
      Country firstCountry = new Country("testName", 1);
      Country secondCountry = new Country("testName", 1);

      // Assert
      Assert.AreEqual(firstCountry, secondCountry);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Country testCountry = new Country("testName");

      //Act
      testCountry.Save();
      Country savedCountry = Country.GetAll()[0];

      int result = savedCountry.GetId();
      int testId = testCountry.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void SaveAndGetAll_SavesToDatabaseAndReturnAll_Country()
    {
      //Arrange
      Country testCountry = new Country("testName");

      //Act
      testCountry.Save();
      List<Country> result = Country.GetAll();
      List<Country> testList = new List<Country>{testCountry};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindCountryInDatabase_Country()
    {
      //Arrange
      Country testCountry = new Country("testName");
      testCountry.Save();

      //Act
      Country resultById = Country.Find(testCountry.GetId());

      //Assert
      Assert.AreEqual(testCountry, resultById);
    }
    [TestMethod]
    public void GetCities_RetrievesAllCitiesWithCountryId_CityList()
    {
      // Arrange
      Country testCountry = new Country("testCountry");
      testCountry.Save();
      City testCity = new City("testCity", testCountry.GetId());
      testCity.Save();
      List<City> testCities = new List<City> {testCity};

      // Act
      List<City> resultCities = testCountry.GetCities();

      // Assert
      CollectionAssert.AreEqual(testCities, resultCities);
    }
  }
}
