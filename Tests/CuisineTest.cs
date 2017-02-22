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
  }
}
