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

    [Fact]
    public void Test_Save_SavesToDataBase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("French Restaurant",1);

      //Act
      testRestaurant.Save();
      List<Restaurant> actualResult = Restaurant.GetAll();
      List<Restaurant> expectedResult = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Test_Save_AssignIdToObjects()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Cactus", 2);

      testRestaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int expectedResult = testRestaurant.GetId();
      int actualResult = savedRestaurant.GetId();

      Assert.Equal(expectedResult, actualResult);
    }
  }
}
