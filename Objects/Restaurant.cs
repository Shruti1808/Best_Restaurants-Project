using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class Restaurant
  {
    private int _id;
    private string _restaurantName;
    private int _cuisineId;
    private string _address;

    public Restaurant(string restaurantName, int cuisineId, string address, int Id = 0)
    {
      _id = Id;
      _restaurantName = restaurantName;
      _address = address;
      _cuisineId = cuisineId;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetRestaurantName()
    {
      return _restaurantName;
    }

    public int GetCuisineId()
    {
      return _cuisineId;
    }

    public string GetAddress()
    {
      return _address;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if(!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = (this.GetId() == newRestaurant.GetId());
        bool nameEquality = (this.GetRestaurantName() == newRestaurant.GetRestaurantName());
        bool cuisineIdEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
        bool addressEquality = (this.GetAddress() == newRestaurant.GetAddress());

        return (idEquality && nameEquality && cuisineIdEquality && addressEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public static void DeleteAll()
    {
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", connection);
      cmd.ExecuteNonQuery();
      connection.Close();
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", connection);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        int cuisineId = rdr.GetInt32(2);
        string address = rdr.GetString(3);

        Restaurant newRestaurant = new Restaurant(restaurantName, cuisineId, address, id);
        allRestaurants.Add(newRestaurant);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (connection != null)
      {
        connection.Close();
      }

      return allRestaurants;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (restaurant_name, cuisine_id, address) OUTPUT INSERTED.id VALUES(@RestaurantName, @CuisineId, @Address);", conn);

      SqlParameter restaurantNameParameter = new SqlParameter();
      restaurantNameParameter.ParameterName = "@RestaurantName";
      restaurantNameParameter.Value = this.GetRestaurantName();

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = this.GetCuisineId();

      SqlParameter addressParameter = new SqlParameter();
      addressParameter.ParameterName = "@Address";
      addressParameter.Value = this.GetAddress();

      cmd.Parameters.Add(restaurantNameParameter);
      cmd.Parameters.Add(cuisineIdParameter);
      cmd.Parameters.Add(addressParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);

      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(restaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName = null;
      string foundAddress = null;
      int foundCuisineId = 0;

      while (rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundCuisineId = rdr.GetInt32(2);
        foundAddress = rdr.GetString(3);
      }

      Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundCuisineId, foundAddress, foundRestaurantId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundRestaurant;
    }

    public void DeleteRestaurant()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants WHERE id = @RestaurantId;", conn);

      cmd.Parameters.Add(new SqlParameter("@RestaurantId", this.GetId()));
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public void Update(string newRestaurantName = null, int newCuisineId = 0, string newAddress = null)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      //new command to change any changed fields
      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET restaurant_name = @newRestaurantName, cuisine_id = @newCuisineId, address = @newAddress OUTPUT INSERTED.restaurant_name, INSERTED.cuisine_id, INSERTED.address WHERE id = @RestaurantId;", conn);

      //Get id of restaurant to use in command
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = this.GetId();
      cmd.Parameters.Add(restaurantIdParameter);

      //CHANGE RESTAURANT NAME
      SqlParameter newRestaurantNameParameter = new SqlParameter();
      newRestaurantNameParameter.ParameterName = "@newRestaurantName";

      //If there is a new restaurant name, change it
      if (newRestaurantName != null)
      {
        newRestaurantNameParameter.Value = newRestaurantName;
      }
      //if there isn't a new restaurant name, don't change the name
      else
      {
        newRestaurantNameParameter.Value = this.GetRestaurantName();
      }
      cmd.Parameters.Add(newRestaurantNameParameter);

      //CHANGE CUISINE ID
      SqlParameter newCuisineIdParameter = new SqlParameter();
      newCuisineIdParameter.ParameterName = "@newCuisineId";

      //If there is a new restaurant name, change it
      if (newCuisineId != 0)
      {
        newCuisineIdParameter.Value = newCuisineId;
      }
      //if there isn't a new restaurant name, don't change the name
      else
      {
        newCuisineIdParameter.Value = this.GetCuisineId();
      }
      cmd.Parameters.Add(newCuisineIdParameter);

      //CHANGE ADDRESS
      SqlParameter newAddressParameter = new SqlParameter();
      newAddressParameter.ParameterName = "@newAddress";

      //If there is a new restaurant name, change it
      if (newAddress != null)
      {
        newAddressParameter.Value = newAddress;
      }
      //if there isn't a new restaurant name, don't change the name
      else
      {
        newAddressParameter.Value = this.GetAddress();
      }
      cmd.Parameters.Add(newAddressParameter);

      //execute reader
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._restaurantName = rdr.GetString(0);
        this._cuisineId = rdr.GetInt32(1);
        this._address = rdr.GetString(2);
      }
      if(rdr!= null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
