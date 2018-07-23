using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TripAdvisor;

namespace TripAdvisor.Models
{
  public class CityActivity
  {
    private int id;
    private int cityID;
    private int activityID;

    public CityActivity (int newCityID, int newActivityID, int newId = 0)
    {
      id = newId;
      cityID = newCityID;
      activityID = newActivityID;
    }
    public int GetId()
    {
      return id;
    }
    public int GetCityID()
    {
      return cityID;
    }
    public int GetActivityID()
    {
      return activityID;
    }
    public override bool Equals(System.Object otherCityActivity)
    {
      if (!(otherCityActivity is CityActivity))
      {
        return false;
      }
      else
      {
        CityActivity newCityActivity = (CityActivity) otherCityActivity;
        bool idEquality = (this.GetId() == newCityActivity.GetId());
        bool cityIDEquality = (this.GetCityID() == newCityActivity.GetCityID());
        bool activityIDEquality = (this.GetActivityID() == newCityActivity.GetActivityID());
        return (idEquality && cityIDEquality && activityIDEquality);
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities_activities (cities_id, activities_id) VALUES (@inputCityID, @inputActivityID);";
      MySqlParameter newCityID = new MySqlParameter();
      newCityID.ParameterName = "@inputCityID";
      newCityID.Value = this.cityID;
      cmd.Parameters.Add(newCityID);
      MySqlParameter newActivityID = new MySqlParameter();
      newActivityID.ParameterName = "@inputActivityID";
      newActivityID.Value = this.activityID;
      cmd.Parameters.Add(newActivityID);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }
    public static CityActivity FindById(int searchId)
    {
      int id = 0;
      int cityID = 0;
      int activityID = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities_activities WHERE id = @idMatch;";
      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@idMatch";
      parameterId.Value = searchId;
      cmd.Parameters.Add(parameterId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        cityID = rdr.GetInt32(1);
        activityID = rdr.GetInt32(2);
      }
      CityActivity foundCityActivity =  new CityActivity(cityID, activityID, id);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundCityActivity;
    }
    public static List<CityActivity> GetAll()
    {
      List <CityActivity> newCityActivities = new List<CityActivity> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities_activities;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int cityID = rdr.GetInt32(1);
        int activityID = rdr.GetInt32(2);
        CityActivity newCityActivity = new CityActivity(cityID, activityID, id);
        newCityActivities.Add(newCityActivity);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newCityActivities;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cities_activities;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
