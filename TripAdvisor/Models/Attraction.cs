// using System;
// using System.Collections.Generic;
// using MySql.Data.MySqlClient;
// using TripAdvisor;
//
// namespace TripAdvisor.Models
// {
//   public class Attraction
//   {
//     private int id;
//     private string name;
//     private int cityId;
//     private string description;
//
//     public Attraction(string newName, int newCityId, string newDescription ="", int newId = 0)
//     {
//       name = newName;
//       cityId = newCityId;
//       description = newDespcription;
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
//     public int GetCityId()
//     {
//       return cityId;
//     }
//
//     public string GetAttractions()
//     {
//       return attraction;
//     }
//
//     public override bool Equals(System.Object otherAttraction)
//     {
//       if(!(otherAttraction is Attraction))
//       {
//         return false;
//       }
//       else
//       {
//         Attraction newAttraction = (Attraction) otherAttraction;
//         bool idEquality = (this.GetId() == newAttraction.GetId());
//         bool nameEquality = this.GetName().Equals(newAttraction.GetName());
//         bool cityIdEquality = this.GetCityId().Equals(newAttraction.GetCityId());
//         bool descriptionEquality = this.GetDescription().Equals(newAttraction.GetDescription());
//         return (idEquality && nameEquality && countryIdEquality && descriptionEquality);
//       }
//     }
//
//     public void Save()
//     {
//       MySqlConnection conn = DB.Connection();
//       conn.Open();
//       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
//       cmd.CommandText = @"INSERT INTO activities (name, city_id, description) VALUES (@attractionName, @cityId, @description);";
//       MySqlParameter newName = new MySqlParameter();
//       newName.ParameterName = "@attractionName";
//       newName.Value = this.name;
//       cmd.Parameters.Add(newName);
//       MySqlParameter newCityId = new MySqlParameter();
//       newCityId.ParameterName = "@cityName";
//       newCityId.Value = this.cityId;
//       cmd.Parameters.Add(newCityId);
//       cmd.ExecuteNonQuery();
//       id = (int) cmd.LastInsertedId;
//       conn.Close();
//       if (conn !=null)
//       {
//         conn.Dispose();
//       }
//     }
//
//     public static List<Attraction> GetAll()
//     {
//       List<Attraction> allAttractions = new List<Attraction> {};
//       MySqlConnection conn = DB.Connection();
//       conn.Open();
//       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
//       cmd.CommandText = @"SELECT * FROM attractions;";
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
