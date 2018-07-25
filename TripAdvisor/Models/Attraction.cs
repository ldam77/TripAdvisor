using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TripAdvisor;

namespace TripAdvisor.Models
{
  public class Attraction
  {
    private int id;
    private string name;
    private int cityId;
    private string description;

    public Attraction(string newName, int newCityId, string newDescription ="", int newId = 0)
    {
      name = newName;
      cityId = newCityId;
      description = newDescription;
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

    public int GetCityId()
    {
      return cityId;
    }

    public string GetDescription()
    {
      return description;
    }

    public override bool Equals(System.Object otherAttraction)
    {
      if(!(otherAttraction is Attraction))
      {
        return false;
      }
      else
      {
        Attraction newAttraction = (Attraction) otherAttraction;
        bool idEquality = (this.GetId() == newAttraction.GetId());
        bool nameEquality = this.GetName().Equals(newAttraction.GetName());
        bool cityIdEquality = this.GetCityId().Equals(newAttraction.GetCityId());
        bool descriptionEquality = this.GetDescription().Equals(newAttraction.GetDescription());
        return (idEquality && nameEquality && cityIdEquality && descriptionEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO attractions (name, city_id, description) VALUES (@attractionName, @cityId, @description);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@attractionName";
      newName.Value = this.name;
      cmd.Parameters.Add(newName);
      MySqlParameter newCityId = new MySqlParameter();
      newCityId.ParameterName = "@cityId";
      newCityId.Value = this.cityId;
      cmd.Parameters.Add(newCityId);
      MySqlParameter newdescription = new MySqlParameter();
      newdescription.ParameterName = "@description";
      newdescription.Value = this.description;
      cmd.Parameters.Add(newdescription);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<Attraction> GetAll()
    {
      List<Attraction> allAttractions = new List<Attraction> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM attractions;";
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

    public static Attraction Find(int inputId)
    {
      int id = 0;
      string name = "";
      int cityId = 0;
      string description = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM attractions WHERE id = @Id;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@Id";
      searchId.Value = inputId;
      cmd.Parameters.Add(searchId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        cityId = rdr.GetInt32(2);
        description = rdr.GetString(3);
      }
      Attraction foundAttraction = new Attraction(name, cityId, description, id);
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return foundAttraction;
    }

    public static List<Attraction> FindByName(string inputName)
    {
      List<Attraction> foundAttractions = new List<Attraction> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM attractions WHERE name LIKE @Name;";
      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@Name";
      searchName.Value = inputName + "%";
      cmd.Parameters.Add(searchName);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int cityId = rdr.GetInt32(2);
        string description = rdr.GetString(3);
        Attraction foundAttraction = new Attraction(name, cityId, description, id);
        foundAttractions.Add(foundAttraction);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return foundAttractions;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM attractions;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
