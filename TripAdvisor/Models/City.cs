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

    public City(string newName, int newCountryId, int = newId)
    {
      name = newName;
      countryId = newCountryId
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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities (name, country_id) VALUES (@cityName, @countryName);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@cityName";
      newName.Value = this.name;
      cmd.Parameters.Add(newName);
      MySqlParameter newCountryId = new MySqlParameter();
      newCountryId.ParameterName = "@countryName";
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

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cities;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
