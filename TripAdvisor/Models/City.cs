using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TripAdvisor;

namespace TripAdvisor.Models
{
  public class City
  {
    private int id;
    private string name;
    private int countryId;

    public City(string newName, int newCountryId, int newId = 0)
    {
      name = newName;
      countryId = newCountryId;
      id = newId;
    }

    public int GetId()
    {
      return id;
    }

    public string GetName()
    {
      return name;
    }

    public int GetCountryId()
    {
      return countryId;
    }

    public override bool Equals(System.Object otherCity)
    {
      if(!(otherCity is City))
      {
        return false;
      }
      else
      {
        City newCity = (City) otherCity;
        bool idEquality = (this.GetId() == newCity.GetId());
        bool nameEquality = this.GetName().Equals(newCity.GetName());
        bool countryIdEquality = this.GetCountryId().Equals(newCity.GetCountryId());
        return (idEquality && nameEquality && countryIdEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities (name, country_id) VALUES (@cityName, @countryId);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@cityName";
      newName.Value = this.name;
      cmd.Parameters.Add(newName);
      MySqlParameter newCountryId = new MySqlParameter();
      newCountryId.ParameterName = "@countryId";
      newCountryId.Value = this.countryId;
      cmd.Parameters.Add(newCountryId);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<City> GetAll()
    {
      List<City> allCities = new List<City> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int countryId = rdr.GetInt32(2);
        City newCity = new City(name, countryId, id);
        allCities.Add(newCity);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allCities;
    }

    public static City Find(int inputId)
    {
      int id = 0;
      string name = "";
      int countryId = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities WHERE id = @Id;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@Id";
      searchId.Value = inputId;
      cmd.Parameters.Add(searchId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        countryId = rdr.GetInt32(2);

      }
      City foundCity = new City(name, countryId, id);
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return foundCity;
    }

    public static List<City> FindByName(string inputName)
    {
      List<City> foundCities = new List<City> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities WHERE name LIKE @Name;";
      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@Name";
      searchName.Value = inputName + "%";
      cmd.Parameters.Add(searchName);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int countryId = rdr.GetInt32(2);
        City foundCity = new City(name, countryId, id);
        foundCities.Add(foundCity);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return foundCities;
    }

    public List<Attraction> GetAttractions()
    {
      List<Attraction> allAttractions = new List<Attraction> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM attractions WHERE city_id = @cityId;";
      MySqlParameter myCityId = new MySqlParameter();
      myCityId.ParameterName = "@cityId";
      myCityId.Value = this.id;
      cmd.Parameters.Add(myCityId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int cityId = rdr.GetInt32(2);
        string description = rdr.GetString(3);
        Attraction newAttraction = new Attraction(name, cityId, description, id);
        allAttractions.Add(newAttraction);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allAttractions;
    }

    public List<Activity> GetActivities()
    {
      List<Activity> allActivities = new List<Activity> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT activities.* FROM activities JOIN cities_activities ON (activities.id = cities_activities.activity_id) JOIN cities ON (cities_activities.city_id = cities.id) WHERE city_id = @cityId;";
      MySqlParameter myCityId = new MySqlParameter();
      myCityId.ParameterName = "@cityId";
      myCityId.Value = this.id;
      cmd.Parameters.Add(myCityId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        Activity newActivity = new Activity(name, description, id);
        allActivities.Add(newActivity);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allActivities;
    }

    public List<Food> GetFoods()
    {
      List<Food> allFoods = new List<Food> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT food.* FROM food JOIN cities_food ON (food.id = cities_food.food_id) JOIN cities ON (cities_food.city_id = cities.id) WHERE city_id = @cityId;";
      MySqlParameter myCityId = new MySqlParameter();
      myCityId.ParameterName = "@cityId";
      myCityId.Value = this.id;
      cmd.Parameters.Add(myCityId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        Food newFood = new Food(name, description, id);
        allFoods.Add(newFood);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allFoods;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cities; DELETE FROM food; DELETE FROM activities;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
