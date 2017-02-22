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

      Get["/cuisines/{id}"] = parameters => {
        Cuisine currentCuisine = Cuisine.Find(parameters.id);
        return View["cuisine.cshtml", currentCuisine];
      };

      Get["/cuisines/delete/{id}"] = parameters => {
        Cuisine currentCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_delete.cshtml", currentCuisine];
      };

      Delete["/cuisines/delete/{id}"] = parameters => {
        Cuisine currentCuisine = Cuisine.Find(parameters.id);
        currentCuisine.DeleteCuisine();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };

      Get["/cuisines/update/{id}"] = parameters => {
        Cuisine currentCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_update.cshtml",currentCuisine];
      };

      Patch["/cuisines/update/{id}"] = parameters => {
        Cuisine currentCuisine = Cuisine.Find(parameters.id);
        currentCuisine.Update(Request.Form["cuisine-name"]);
        return View ["cuisine.cshtml", currentCuisine];
      };

    }
  }
}
