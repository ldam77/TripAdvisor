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

    public Country(string newName, int = newId)
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
  }
}
