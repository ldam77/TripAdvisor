using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TripAdvisor;

namespace TripAdvisor.Models
{
  public class Activity
  {
    private int id;
    private string name;
    private string description;

    public Activity(string newName, string newDescription, int newId = 0)
    {
      name = newName;
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
    public string GetDescription()
    {
      return description;
    }

    public override bool Equals(System.Object otherActivity)
    {
      if (!(otherActivity is Activity))
      {
       return false;
      }
      else
      {
        Activity newActivity = (Activity) otherActivity;
        bool idEqual = (this.GetId() == newActivity.GetId());
        bool nameEqual = (this.GetName() == newActivity.GetName());
        bool descriptionEqual = (this.GetDescription() == newActivity.GetDescription());
        return (idEqual && nameEqual && descriptionEqual);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO activities (name, description) VALUES (@activityName, @activityDescription);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@activityName";
      newName.Value = this.name;
      cmd.Parameters.Add(newName);
      MySqlParameter newDescription = new MySqlParameter();
      newDescription.ParameterName = "@activityDescription";
      newDescription.Value = this.description;
      cmd.Parameters.Add(newDescription);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<Activity> GetAll()
    {
      List<Activity> allActivities = new List<Activity> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM activities;";
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

    public static Activity FindByActivityId(int byId)
    {
      int id = 0;
      string name = "";
      string description = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM activities WHERE id = @idFound;";
      MySqlParameter foundId = new MySqlParameter();
      foundId.ParameterName = "@idFound";
      foundId.Value = byId;
      cmd.Parameters.Add(foundId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        description = rdr.GetString(2);
      }
      Activity newActivity = new Activity(name, description, id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newActivity;
    }

    public static List<Activity> FindByActivityName(string activityName)
    {
      List<Activity> foundActivity = new List<Activity> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM activities WHERE name LIKE @foundName;";
      MySqlParameter foundActivityName = new MySqlParameter();
      foundActivityName.ParameterName = "@foundName";
      foundActivityName.Value = activityName + "%";
      cmd.Parameters.Add(foundActivityName);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
       {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        Activity newActivity = new Activity(name, description, id);
        foundActivity.Add(newActivity);
       }
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
       return foundActivity;
    }

    public void EditActivity(string newName, string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE activities SET name = @name, description = @activityDescription WHERE id = @idMatches;";

      MySqlParameter editName = new MySqlParameter();
      editName.ParameterName = "@name";
      editName.Value = newName;
      cmd.Parameters.Add(editName);

      MySqlParameter editDescription = new MySqlParameter();
      editDescription.ParameterName = "@activityDescription";
      editDescription.Value = newDescription;
      cmd.Parameters.Add(editDescription);

      MySqlParameter foundId = new MySqlParameter();
      foundId.ParameterName = "@idMatches";
      foundId.Value = this.id;
      cmd.Parameters.Add(foundId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM activities;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
       {
         conn.Dispose();
       }
    }
  }
}
