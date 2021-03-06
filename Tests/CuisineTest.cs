using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class CuisineTest: IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Cuisine.DeleteAll();
    }

    [Fact]
    public void Test_CuisineTableEmptyAtFirst()
    {
      //Arrange Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0,result);
    }

    [Fact]
    public void Test_EqualOverrideTrueIfCuisineNameIsSame()
    {
      Cuisine firstCuisineName = new Cuisine("French cuisine");
      Cuisine secondCuisineName = new Cuisine("French cuisine");

      Assert.Equal(firstCuisineName,secondCuisineName);
    }

    [Fact]
    public void Test_Save_SavesToDataBase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("French Cuisine");

      //Act
      testCuisine.Save();
      List<Cuisine> actualResult = Cuisine.GetAll();
      List<Cuisine> expectedResult = new List<Cuisine>{testCuisine};

      //Assert
      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Test_Save_AssignIdToObjects()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("French Cuisine");

      testCuisine.Save();
      Cuisine savedCuisine = Cuisine.GetAll()[0];

      int result = savedCuisine.GetId();
      int testId = testCuisine.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsCuisineInDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("French Cuisine");
      testCuisine.Save();

      //Act
      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

      //Assert
      Assert.Equal(testCuisine, foundCuisine);
    }

    [Fact]
    public void Test_DeleteCuisine_DeletesCuisineFromDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("French Cuisine");

      //Act
      testCuisine.Save();
      testCuisine.DeleteCuisine();

      //Assert
      List<Cuisine> expectedResult = new List<Cuisine>{};
      List<Cuisine> actualResult = Cuisine.GetAll();

      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Test_Update_UpdatesCuisineInDatabase()
    {
      //Arrange
      string CuisineName = "French Food";
      Cuisine testCuisine = new Cuisine(CuisineName);
      testCuisine.Save();

      string newCuisineName = "French Cuisine";

      //Act
      testCuisine.Update(newCuisineName);
      string actualResult = testCuisine.GetCuisineName();
      string expectedResult = "French Cuisine";

      //Assert
      Assert.Equal(expectedResult,actualResult);
    }

    [Fact]
    public void Test_GetRestaurants_ListsRestaurantsInCuisine()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine ("French Cuisine");
      testCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("Le Fromage", testCuisine.GetId(), "24 Main St.", "10AM", "11PM");
      Restaurant secondRestaurant = new Restaurant("Le Pichet", testCuisine.GetId(), "5 First St.", "10AM", "11PM");
      Restaurant thirdRestaurant = new Restaurant("La Menagerie", testCuisine.GetId(), "155 Spring St.", "10AM", "11PM");

      //Act
      // Console.WriteLine(testCuisine.GetId());
      firstRestaurant.Save();
      secondRestaurant.Save();
      thirdRestaurant.Save();


      //Assert
      List<Restaurant> expectedResult = new List<Restaurant> {firstRestaurant, secondRestaurant, thirdRestaurant};
      List<Restaurant> actualResult = testCuisine.GetRestaurants();

      Assert.Equal(expectedResult, actualResult);
    }
  }
}
