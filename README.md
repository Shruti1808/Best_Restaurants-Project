# Best Restaurants List
#### _Internet Movie Database that is not IMDB_

#### By _**Alexandra Holcombe && Renee Mei && Shruti Priya**_

## Description

This website will let users enter and save movies along with affiliated actors and characters.

## Setup/Installation Requirements

* Requires DNU, DNX, and Mono
* Clone to local machine
* Use command "dnu restore" in command prompt/shell
* Use command "dnx kestrel" to start server
* Navigate to http://localhost:5004 in web browser of choice

## Specifications

**The user can add a cuisine**
* Example Input: "French Cuisine"
* Example Output: "French Cuisine"

**The user can add to a list of multiple cuisines**
* Example Input: "Mexican"
* Example Output: "French Cuisine" "Mexican Comida"

**The user can look at a page of a list of existing cuisines**
* Example Input: *page clicky*
* Example Output: "French Cuisine" "Mexican Comida"

**The user can update/edit a cuisine**
* Example Input: "Mejican Comida"
* Example Output: "Mexican Comida"

**The user can delete a cuisine**
* Example Input: *delete clicky*
* Example Output: "French Cuisine" ONLY

**The user can add a new restaurant to a cuisine**
* Example Input: "French Cuisine" "Restaurant du Fromage"
* Example Output: "French Cuisine Restaurants: Restaurant du Fromage"

**The user can edit a restaurant**
* Example Input: "French Cuisine" "Restaurant du Cheese"
* Example Output: "French Cuisine Restaurants: Restaurant du Fromage"

**The user can delete a restaurant**
* Example Input: *delete clicky*
* Example Output: "French Cuisine Restaurants: NONE THERE IS NO CULTURE HERE"

**The user can click on a restaurant to see its information**
* Example Input: *page clicky*
* Example Output: "Restaurant du Fromage, 42 Champs Elysees, Hours: 3-midnight, signature dish"

**The user can search for all of a cuisine's restaurants**
* Example Input: *french clicky*
* Example Output: "French Cuisine Restaurants: Restaurant du Fromage, Le Pichet"

### Icebox

**The user can add a star review and a text review**
* Example Input: "THIS RESTAURANT IS AWESOME 5 STARS"
* Example Output: "Reviews: THIS RESTAURANT IS AWESOME 5 STARS"

**The website will output an average star value**
* Example Input: "5 stars" "3 stars"
* Example Output: "4 stars"

**The user can choose a price range for each cuisine**
* Example Input: *cheap clicky*
* Example Output: "French Cuisine Restaurants: only the cheap ones"

**The user cannot add a duplicate cuisine**
* Example Input: "French Cuisine"
* Example Output: "Error: This cuisine is already on the list"

**The user cannot add a duplicate restaurant**
* Example Input: "Le Fromage"
* Example Output: "Error: This restaurant is already on the list"

**When the user changes the restaurant's cuisine, the update method for restaurants will return the updated cuisine**
* Example Input: "French Cuisine: Le Fromage"
* Example Output: "Mexican Comida: Le Fromage"

## Support and contact details

Please contact Allie Holcombe at alexandra.holcombe@gmail.com, Renee Mei at dontemailme@dontemailme.com, or Shruti Priya at shrutipriya1808@gmail.com with any questions, concerns, or suggestions.

## Technologies Used

This web application uses:
* Nancy
* Mono
* DNVM
* C#
* Razor

### License

*This project is licensed under the MIT license.*

Copyright (c) 2017 **_Alexandra Holcombe && Renee Mei && Shruti Priya_**
