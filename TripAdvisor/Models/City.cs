// using System;
// using System.Collections.Generic;
// using MySql.Data.MySqlClient;
// using TripAdvisor;
//
// namespace TripAdvisor.Models
// {
//   public class City
//   {
//     private int id;
//     private string name;
//     private int countryId;
//
//     public City(string newName, int newCountryId, int newId = 0)
//     {
//       name = newName;
//       countryId = newCountryId;
//       id = newId;
//     }
//
//     public int GetId()
//     {
//       return id;
//     }
//
//     public string GetName()
//     {
//       return name;
//     }
//
//     public int GetCountryId()
//     {
//       return countryId;
//     }
//
//     public override bool Equals(System.Object otherCity)
//     {
//       if(!(otherCity is City))
//       {
//         return false;
//       }
//       else
//       {
//         City newCity = (City) otherCity;
//         bool idEquality = (this.GetId() == newCity.GetId());
//         bool nameEquality = this.GetName().Equals(newCity.GetName());
//         bool countryIdEquality = this.GetCountryId().Equals(newCity.GetCountryId());
//         return (idEquality && nameEquality && countryIdEquality);
//       }
//     }
//
//     public void Save()
//     {
//       MySqlConnection conn = DB.Connection();
//       conn.Open();
//       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
//       cmd.CommandText = @"INSERT INTO cities (name, country_id) VALUES (@cityName, @countryId);";
//       MySqlParameter newName = new MySqlParameter();
//       newName.ParameterName = "@cityName";
//       newName.Value = this.name;
//       cmd.Parameters.Add(newName);
//       MySqlParameter newCountryId = new MySqlParameter();
//       newCountryId.ParameterName = "@countryId";
//       newCountryId.Value = this.countryId;
//       cmd.Parameters.Add(newCountryId);
//       cmd.ExecuteNonQuery();
//       id = (int) cmd.LastInsertedId;
//       conn.Close();
//       if (conn !=null)
//       {
//         conn.Dispose();
//       }
//     }
//
//     public static List<City> GetAll()
//     {
//       List<City> allCities = new List<City> {};
//       MySqlConnection conn = DB.Connection();
//       conn.Open();
//       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
//       cmd.CommandText = @"SELECT * FROM cities;";
//       MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
//       while(rdr.Read())
//       {
//         int id = rdr.GetInt32(0);
//         string name = rdr.GetString(1);
//         int countryId = rdr.GetInt32(2);
//         City newCity = new City(name, countryId, id);
//         allCities.Add(newCity);
//       }
//       conn.Close();
//       if (conn !=null)
//       {
//         conn.Dispose();
//       }
//       return allCities;
//     }
//
//     public List<Attraction> GetAttractions()
//     {
//       List<Attraction> allAttractions = new List<Attraction> {};
//       MySqlConnection conn = DB.Connection();
//       conn.Open();
//       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
//       cmd.CommandText = @"SELECT * FROM attractions WHERE city_id = @cityId;";
//       MySqlParameter myCityId = new MySqlParameter();
//       myCityId.ParameterName = "@cityId";
//       myCityId.Value = this.id;
//       cmd.Parameters.Add(myCityId);
//       MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
//       while(rdr.Read())
//       {
//         int id = rdr.GetInt32(0);
//         string name = rdr.GetString(1);
//         int cityId = rdr.GetInt32(2);
//         string description = rdr.GetString(3);
//         Attraction newAttraction = new Attraction(name, cityId, description, id);
//         allAttractions.Add(newAttraction);
//       }
//       conn.Close();
//       if (conn !=null)
//       {
//         conn.Dispose();
//       }
//       return allAttractions;
//     }
//
//     public static void DeleteAll()
//     {
//       MySqlConnection conn = DB.Connection();
//       conn.Open();
//
//       var cmd = conn.CreateCommand() as MySqlCommand;
//       cmd.CommandText = @"DELETE FROM cities;";
//       cmd.ExecuteNonQuery();
//
//       conn.Close();
//       if (conn != null)
//       {
//         conn.Dispose();
//       }
//     }
//   }
// }
