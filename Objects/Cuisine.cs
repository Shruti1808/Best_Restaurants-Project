using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class Cuisine
  {
    private int _id;
    private string _cuisineName;

    public Cuisine(string cuisineName, int Id=0)
    {
      _id = Id;
      _cuisineName = cuisineName;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetCuisineName()
    {
      return _cuisineName;
    }

    public void SetCuisineName(string newCuisineName)
    {
      _cuisineName = newCuisineName;
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public static void DeleteAll()
    {
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines;", connection);
      cmd.ExecuteNonQuery();
      connection.Close();
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine>{};
      SqlConnection connection = DB.Connection();
      connection.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines;", connection);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string cuisineName = rdr.GetString(1);

        Cuisine newCuisine = new Cuisine(cuisineName, id);
        allCuisines.Add(newCuisine);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(connection != null)
      {
        connection.Close();
      }

      return allCuisines;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = (this.GetId()== newCuisine.GetId());
        bool nameEquality = (this.GetCuisineName()== newCuisine.GetCuisineName());

        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisines (cuisine_name) OUTPUT INSERTED.id VALUES(@CuisineName);", conn);

      SqlParameter cuisineNameParameter = new SqlParameter();
      cuisineNameParameter.ParameterName = "@CuisineName";
      cuisineNameParameter.Value = this.GetCuisineName();
      cmd.Parameters.Add(cuisineNameParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
        // this._cuisineName = rdr.GetString(1);
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

    public static Cuisine Find(int Id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines WHERE id = @CuisineId;", conn);
      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = Id.ToString();
      cmd.Parameters.Add(cuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCuisineId = 0;
      string foundCuisineName = null;

      while (rdr.Read())
      {
        foundCuisineId = rdr.GetInt32(0);
        foundCuisineName = rdr.GetString(1);
      }

      Cuisine foundCuisine = new Cuisine(foundCuisineName, foundCuisineId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if ( conn != null)
      {
        conn.Close();
      }

      return foundCuisine;
    }

    public void DeleteCuisine()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines WHERE id = @CuisineId;", conn);
      cmd.Parameters.Add(new SqlParameter("@CuisineId", this.GetId()));
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public void Update(string newCuisineName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE cuisines Set cuisine_name = @newCuisineName OUTPUT INSERTED.cuisine_name WHERE id = @CuisineId;", conn);

      SqlParameter newCuisineNameParameter = new SqlParameter();
      newCuisineNameParameter.ParameterName = "@newCuisineName";
      newCuisineNameParameter.Value = newCuisineName;
      cmd.Parameters.Add(newCuisineNameParameter);

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = this.GetId();
      cmd.Parameters.Add(cuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._cuisineName = rdr.GetString(0);
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

    public List<Restaurant> GetRestaurants()
    {
      List<Restaurant> CuisineRestaurants = new List<Restaurant>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE cuisine_id = @CuisineId;", conn);
      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = this.GetId();
      cmd.Parameters.Add(cuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        int restaurantCuisineId = rdr.GetInt32(2);
        string restaurantAddress = rdr.GetString(3);
        string restaurantOpenTime = rdr.GetString(4);
        string restaurantCloseTime = rdr.GetString(5);

        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantAddress, restaurantOpenTime, restaurantCloseTime, restaurantId);
        CuisineRestaurants.Add(newRestaurant);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return CuisineRestaurants;
    }
  }
}
