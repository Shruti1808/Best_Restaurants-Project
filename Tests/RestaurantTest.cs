using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class RestaurantTest: IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

    [Fact]
    public void Test_RestaurantTableEmptyAtFirst()
    {
      //Arrange Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0,result);
    }

    [Fact]
    public void Test_EqualOverrideTrueIfRestaurantNameIsSame()
    {
      Restaurant firstRestaurantName = new Restaurant("Le Fromage", 1);
      Restaurant secondRestaurantName = new Restaurant("Le Fromage", 1);

      Assert.Equal(firstRestaurantName, secondRestaurantName);
    }
  }
}
