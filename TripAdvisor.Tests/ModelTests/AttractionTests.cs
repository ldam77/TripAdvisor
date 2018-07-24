using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripAdvisor.Models;
using System;
using System.Collections.Generic;

namespace TripAdvisor.Tests
{
  [TestClass]
  public class AttractionTests : IDisposable
  {
    public void Dispose()
    {
      Attraction.DeleteAll();
    }
    public AttractionTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=trip_advisor_test;";
    }
    [TestMethod]
    public void GetTest_ReturnDataField()
    {
      // Arrange
      int id = 1;
      string name = "testAttraction";
      int cityId = 1;
      string description = "";
      Attraction testAttraction = new Attraction(name, cityId, description, id);

      // Act
      int resultId = testAttraction.GetId();
      string resultName = testAttraction.GetName();
      int resultCityId = testAttraction.GetCityId();
      string resultDescription = testAttraction.GetDescription();

      // Assert
      Assert.AreEqual(id, resultId);
      Assert.AreEqual(name, resultName);
      Assert.AreEqual(cityId, resultCityId);
      Assert.AreEqual(description, resultDescription);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfIdAndNameAreTheSame_Attraction()
    {
      // Arrange, Act
      Attraction firstAttraction = new Attraction("testName", 1, "testDescription", 1);
      Attraction secondAttraction = new Attraction("testName", 1, "testDescription", 1);

      // Assert
      Assert.AreEqual(firstAttraction, secondAttraction);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Attraction testAttraction = new Attraction("testName", 1);

      //Act
      testAttraction.Save();
      Attraction savedAttraction = Attraction.GetAll()[0];

      int result = savedAttraction.GetId();
      int testId = testAttraction.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void SaveAndGetAll_SavesToDatabaseAndReturnAll_Attraction()
    {
      //Arrange
      Attraction testAttraction = new Attraction("testName", 1);

      //Act
      testAttraction.Save();
      List<Attraction> result = Attraction.GetAll();
      List<Attraction> testList = new List<Attraction>{testAttraction};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindAttractionInDatabase_Attraction()
    {
      //Arrange
      Attraction testAttraction = new Attraction("testName", 1);
      testAttraction.Save();

      //Act
      Attraction resultById = Attraction.Find(testAttraction.GetId());

      //Assert
      Assert.AreEqual(testAttraction, resultById);
    }
  }
}
