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

      Get["/cuisines/{id}/restaurants/new"] = parameters => {
        Cuisine currentCuisine = Cuisine.Find(parameters.id);
        return View["restaurant_form.cshtml", currentCuisine];
      };

      Post["/cuisines/{id}/restaurants/new"] = parameters => {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], parameters.id,"address");
        newRestaurant.Save();
        return View["cuisine.cshtml", Cuisine.Find(parameters.id)];
      };

      Get["/cuisines/{cuisineId}/restaurants/{id}"] = parameters => {
        Restaurant currentRestaurant = Restaurant.Find(parameters.id);
        Cuisine currentCuisine = Cuisine.Find(parameters.cuisineId);
        Dictionary<string, object> model = new Dictionary<string, object>(){{"restaurant", currentRestaurant}, {"cuisine", currentCuisine}};
        return View["restaurant_info.cshtml", model];
      };

      Get["/cuisines/{cuisineId}/restaurants/{id}/delete"] = parameters => {
        Restaurant currentRestaurant = Restaurant.Find(parameters.id);
        Cuisine currentCuisine = Cuisine.Find(parameters.cuisineId);
        Dictionary<string, object> model = new Dictionary<string, object>(){{"restaurant", currentRestaurant}, {"cuisine", currentCuisine}};
        return View["restaurant_delete.cshtml", model];
      };

      Delete["/cuisines/{cuisineId}/restaurants/{id}/delete"] = parameters => {
        Restaurant currentRestaurant = Restaurant.Find(parameters.id);
        Cuisine currentCuisine = Cuisine.Find(parameters.cuisineId);
        currentRestaurant.DeleteRestaurant();
        // List<Restaurant> CuisineRestaurants = currentCuisine.GetRestaurants();
        return View["cuisine.cshtml", currentCuisine];
      };

      Get["/cuisines/{cuisineId}/restaurants/{id}/update"] = parameters => {
        Restaurant currentRestaurant = Restaurant.Find(parameters.id);
        List<Cuisine> allCuisines = Cuisine.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>(){{"restaurant", currentRestaurant}, {"cuisines", allCuisines}};
        return View["restaurant_update.cshtml", model];
      };

      Patch["/cuisines/{cuisineId}/restaurants/{id}/update"] = parameters => {
        Restaurant currentRestaurant = Restaurant.Find(parameters.id);
        currentRestaurant.Update(Request.Form["restaurant-name"], Request.Form["cuisine-name"], "newAddress");
        Cuisine currentCuisine = Cuisine.Find(currentRestaurant.GetCuisineId());
        Dictionary<string, object> model = new Dictionary<string, object>{{"restaurant", currentRestaurant},{"cuisine", currentCuisine}};
        return View["restaurant_info.cshtml", model];
      };
    }
  }
}
