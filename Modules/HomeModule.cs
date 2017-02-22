using Nancy;
using System;
using System.Collections.Generic;
using BestRestaurants;

namespace BestRestaurants
{
  public class HomeModule: NancyModule
  {
    public HomeModule()
    {
      Get["/"] =_=> {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
      Post["/cuisine/new"] =_=> {
        string newCuisineName = Request.Form["cuisine-name"];
        Cuisine newCuisine = new Cuisine(newCuisineName);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
    }
  }
}
