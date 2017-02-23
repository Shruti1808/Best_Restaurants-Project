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
    public void Test_EqualOverrideTrueIfRestaurantNameAddressTimeIdIsSame()
    {
      Restaurant firstRestaurantName = new Restaurant("Le Fromage", 1, "24 Main St.", "10AM", "11PM");
      Restaurant secondRestaurantName = new Restaurant("Le Fromage", 1, "24 Main St.", "10AM", "11PM");

      Assert.Equal(firstRestaurantName, secondRestaurantName);
    }

    [Fact]
    public void Test_Save_SavesToDataBase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("French Restaurant",1, "24 Main St.", "10AM", "11PM");

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
      Restaurant testRestaurant = new Restaurant("Cactus", 2, "24 Main St.", "10AM", "11PM");

      testRestaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int expectedResult = testRestaurant.GetId();
      int actualResult = savedRestaurant.GetId();

      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Le Fromage", 1, "24 Main St.", "10AM", "11PM");
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }

    [Fact]
    public void Test_DeleteRestaurant_DeleteRestaurantFromDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Le Fromage", 1, "24 Main St.", "10AM", "11PM");

      //Act
      testRestaurant.Save();
      testRestaurant.DeleteRestaurant();

      //Assert
      List<Restaurant> expectedResult = new List<Restaurant>{};
      List<Restaurant> actualResult = Restaurant.GetAll();

      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Test_Update_UpdateRestaurantInDatabase()
    {
      string RestaurantName = "Le Rmoges";
      Restaurant testRestaurant = new Restaurant(RestaurantName, 2, "24 MAIN St.", "10AM", "11PM");
      testRestaurant.Save();

      string newRestaurantName ="Le Fromage";
      int newCuisineId = 1;
      string newAddress = "24 Main St.";
      string newOpenTime = "11AM";
      string newCloseTime = "11PM";

      testRestaurant.Update(newRestaurantName, newCuisineId, newAddress, newOpenTime, newCloseTime);
      Restaurant actualResult = testRestaurant;
      Restaurant expectedResult = new Restaurant(newRestaurantName, newCuisineId, newAddress, newOpenTime, newCloseTime, testRestaurant.GetId());

      Assert.Equal(expectedResult,actualResult);
    }
  }
}
