using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TripAdvisor;

namespace TripAdvisor.Models
{
  public class CityFood
  {
    private int id;
    private int cityID;
    private int foodID;

    public CityFood (int newCityID, int newFoodID, int newId = 0)
    {
      id = newId;
      cityID = newCityID;
      foodID = newFoodID;
    }
    public int GetId()
    {
      return id;
    }
    public int GetCityID()
    {
      return cityID;
    }
    public int GetFoodID()
    {
      return foodID;
    }
    public override bool Equals(System.Object otherCityFood)
    {
      if (!(otherCityFood is CityFood))
      {
        return false;
      }
      else
      {
        CityFood newCityFood = (CityFood) otherCityFood;
        bool idEquality = (this.GetId() == newCityFood.GetId());
        bool cityIDEquality = (this.GetCityID() == newCityFood.GetCityID());
        bool foodIDEquality = (this.GetFoodID() == newCityFood.GetFoodID());
        return (idEquality && cityIDEquality && foodIDEquality);
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities_food (city_id, food_id) VALUES (@inputCityID, @inputFoodID);";
      MySqlParameter newCityID = new MySqlParameter();
      newCityID.ParameterName = "@inputCityID";
      newCityID.Value = this.cityID;
      cmd.Parameters.Add(newCityID);
      MySqlParameter newFoodID = new MySqlParameter();
      newFoodID.ParameterName = "@inputFoodID";
      newFoodID.Value = this.foodID;
      cmd.Parameters.Add(newFoodID);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }
    public static CityFood FindById(int searchId)
    {
      int id = 0;
      int cityID = 0;
      int foodID = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities_food WHERE id = @idMatch;";
      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@idMatch";
      parameterId.Value = searchId;
      cmd.Parameters.Add(parameterId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        cityID = rdr.GetInt32(1);
        foodID = rdr.GetInt32(2);
      }
      CityFood foundCityFood =  new CityFood(cityID, foodID, id);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundCityFood;
    }
    public static List<CityFood> GetAll()
    {
      List <CityFood> newCityActivities = new List<CityFood> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities_food;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int cityID = rdr.GetInt32(1);
        int foodID = rdr.GetInt32(2);
        CityFood newCityFood = new CityFood(cityID, foodID, id);
        newCityActivities.Add(newCityFood);
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
      cmd.CommandText = @"DELETE FROM cities_food;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
