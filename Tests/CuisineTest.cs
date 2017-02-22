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
    public void Test_Save_AssignsIdToObjects()
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
  }
}
