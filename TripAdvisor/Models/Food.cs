using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TripAdvisor;

namespace TripAdvisor.Models
{
  public class Food
  {
    private int _id;
    private string _name;
    private string _description;

    public Food(string name, string description, int id = 0)
    {
      _name = name;
      _id = id;
      _description = description;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetDescription()
    {
      return _description;
    }

    public override bool Equals(System.Object otherFood)
    {
      if (!(otherFood is Food))
      {
       return false;
      }
      else
      {
        Food newFood = (Food) otherFood;
        bool idEqual = (this.GetId() == newFood.GetId());
        bool nameEqual = (this.GetName() == newFood.GetName());
        bool descriptionEqual = (this.GetDescription() == newFood.GetDescription());
        return (idEqual && nameEqual && descriptionEqual);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO food (name, description) VALUES (@name, @description);";
      cmd.Parameters.Add(new MySqlParameter("@name", _name));
      cmd.Parameters.Add(new MySqlParameter("@description", _description));

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Food> GetAll()
  {
    List<Food> allFood = new List<Food> {};
    MySqlConnection conn = DB.Connection();
    conn.Open();
    MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM food;";
    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    while(rdr.Read())
    {
      int foodId = rdr.GetInt32(0);
      string foodName = rdr.GetString(1);
      string foodDescription = rdr.GetString(2);
      Food newFood = new Food(foodName, foodDescription, foodId);
      allFood.Add(newFood);
    }
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
    return allFood;
  }
////
  public static Food Find(int id)
 {
   MySqlConnection conn = DB.Connection();
   conn.Open();
   var cmd = conn.CreateCommand() as MySqlCommand;
   cmd.CommandText = @"SELECT * FROM food WHERE id = @thisId;";
   MySqlParameter thisId = new MySqlParameter();
   thisId.ParameterName = "@thisId";
   thisId.Value = id;
   cmd.Parameters.Add(thisId);

   var rdr = cmd.ExecuteReader() as MySqlDataReader;
   int foodId = 0;
   string foodName = "";
   string foodDescription = "";

   while (rdr.Read())
   {
     foodId = rdr.GetInt32(0);
     foodName= rdr.GetString(1);
     foodDescription = rdr.GetString(2);

   }
   Food foundFood = new Food(foodName, foodDescription, foodId);

   conn.Close();
   if (conn != null)
   {
     conn.Dispose();
   }
   return foundFood;
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

public List<City> GetCitiesbyFoodId()
{
  List<City> allCities = new List<City> {};
  MySqlConnection conn = DB.Connection();
  conn.Open();
  MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
  cmd.CommandText = @"SELECT cities.* FROM food
  JOIN cities_food ON (food.id = cities_food.food_id)
  JOIN cities ON (cities_food.city_id = cities.id)
  WHERE food.id = @foodId;";
  MySqlParameter myFoodId = new MySqlParameter();
  myFoodId.ParameterName = "@foodId";
  myFoodId.Value = this._id;
  cmd.Parameters.Add(myFoodId);
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
  cmd.CommandText = @"DELETE FROM food;";
  cmd.ExecuteNonQuery();
  conn.Close();
  if (conn != null)
  {
    conn.Dispose();
  }
}

public void Edit(string newName, string newDescription)
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"UPDATE food SET name = @name, description = @description WHERE id = @foodId;";

    MySqlParameter foodId = new MySqlParameter();
    foodId.ParameterName = "@foodId";
    foodId.Value = this._id;
    cmd.Parameters.Add(foodId);

    MySqlParameter changeName = new MySqlParameter();
    changeName.ParameterName = "@name";
    changeName.Value = newName;
    cmd.Parameters.Add(changeName);

    MySqlParameter changeDescription = new MySqlParameter();
    changeDescription.ParameterName = "@description";
    changeDescription.Value = newDescription;
    cmd.Parameters.Add(changeDescription);

    cmd.ExecuteNonQuery();

    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
  }



}
}
