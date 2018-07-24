using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TripAdvisor;

namespace TripAdvisor.Models
{
  public class Country
  {
    private int id;
    private string name;

    public Country(string newName, int newId = 0)
    {
      name = newName;
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

    public override bool Equals(System.Object otherCountry)
    {
      if(!(otherCountry is Country))
      {
        return false;
      }
      else
      {
        Country newCountry = (Country) otherCountry;
        bool idEquality = (this.GetId() == newCountry.GetId());
        bool nameEquality = this.GetName().Equals(newCountry.GetName());
        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO countries (name) VALUES (@countriesName);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@countriesName";
      newName.Value = this.name;
      cmd.Parameters.Add(newName);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<Country> GetAll()
    {
      List<Country> allCountries = new List<Country> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM countries;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Country newCountry = new Country(name, id);
        allCountries.Add(newCountry);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allCountries;
    }

    public static Country Find(int inputId)
    {
      int id = 0;
      string name = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM countries WHERE id = @Id;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@Id";
      searchId.Value = inputId;
      cmd.Parameters.Add(searchId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      Country foundCountry = new Country(name, id);
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return foundCountry;
    }

    public List<City> GetCities()
    {
      List<City> allCities = new List<City> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities WHERE country_id = @countryId;";
      MySqlParameter myCountryId = new MySqlParameter();
      myCountryId.ParameterName = "@countryId";
      myCountryId.Value = this.id;
      cmd.Parameters.Add(myCountryId);
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
      cmd.CommandText = @"DELETE FROM countries;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
