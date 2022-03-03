using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinewoodGrow.Models;
using PinewoodGrow.Utilities;

namespace PinewoodGrow.Data.Repositorys
{
  

    /*internal struct Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    internal struct Geometry
    {
        public Location location { get; set; }
    }

    internal struct GroceryData
    {
        public Geometry geometry { get; set; }
        public string name { get; set; } 
        public string place_id { get; set; }
        public string scope { get; set; }
        public string vicinity { get; set; }
    }

    public struct Root
    {
        public List<object> html_attributions { get; set; }
        public string next_page_token { get; set; }
        internal List<GroceryData> results { get; set; }
        public string status { get; set; }

    }*/
    public  class  TravelDataRepository 
    {
        private static readonly HttpClient client = new HttpClient();
        private const int radius = 2000;


        public TravelDataRepository()
        {
            client.BaseAddress =
                new Uri("https://maps.googleapis.com/maps/api/place/nearbysearch");
        }

        public static async Task<Tuple<List<TravelDetail>, List<GroceryStore>>> GetAllDetails(List<Address> addresses, List<GroceryStore> stores)
        {
     

            var details = new List<TravelDetail>();
       

            foreach (var address in addresses)
            {
                var (travel, _stores) = await getData(address, stores);
        
                details.Add(travel);
                stores = _stores;
            }

            return Tuple.Create(details, stores);
        }
        

            public static async Task<Tuple<TravelDetail, List<GroceryStore>>> getData(Address address, List<GroceryStore> groceryStores)
        {
            var request =
                $"/maps/api/place/nearbysearch/json?location={address.Latitude},%20{address.Longitude}&rankby=distance&keyword=supermarket%20store&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&";

            var response = await client.GetAsync(request);
            if (!response.IsSuccessStatusCode) throw new Exception("Could not access the Nearest Grocery Stores");

            var jsonString = await response.Content.ReadAsStringAsync();
             Console.WriteLine(JsonConvert.DeserializeObject<object>(jsonString));

            var root = await response.Content.ReadAsAsync<Root>();

            var grocery = root.results[0];

            if (groceryStores.All(g => g.ID != grocery.place_id))
            {
                groceryStores.Add(new GroceryStore
                {
                    ID = grocery.place_id,
                    Name = grocery.name,
                    FullAddress = grocery.vicinity,
                    Latitude = grocery.geometry.location.lat,
                    Longitude = grocery.geometry.location.lng,
                });

            }


            var travel = new TravelDetail
            {
                GroceryID = grocery.place_id,
                AddressID = address.ID,
            };


            string[] travelTypes = { "driving", "bicycling", "walking"};
            var growID = "ChIJN7B0Fp9D04kRjFOKVQ0oblk";

            foreach (var type in travelTypes)
            {
                var Distancerequest = $"/maps/api/distancematrix/json?origins=place_id:{address.PlaceID}&destinations=place_id:{growID}%7Cplace_id:{travel.GroceryID}&mode={type}&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&";
                var Distanceresponse = await client.GetAsync(Distancerequest);
                if (!Distanceresponse.IsSuccessStatusCode) throw new Exception("Could not Get Distance matrix");
                var values = JObject.Parse(await Distanceresponse.Content.ReadAsStringAsync());

            
                switch (type)
                {
                    case "driving":
                        travel.GrowDrive = ((double)values["rows"][0]["elements"][0]["duration"]["value"]);
                        travel.GrowDistance = ((double)values["rows"][0]["elements"][0]["distance"]["value"]);

                        travel.GroceryDrive = ((double)values["rows"][0]["elements"][1]["duration"]["value"]);
                        travel.GroceryDistance = ((double)values["rows"][0]["elements"][1]["distance"]["value"]);
                        break;
                    case "bicycling":
                        travel.GrowBike = ((double)values["rows"][0]["elements"][0]["duration"]["value"]);
                        travel.GroceryBike = ((double)values["rows"][0]["elements"][1]["duration"]["value"]);
                        break;
                    default:
                        travel.GrowWalk = ((double)values["rows"][0]["elements"][0]["duration"]["value"]);
                        travel.GroceryWalk = ((double)values["rows"][0]["elements"][1]["duration"]["value"]);
                        break;
                }

            }

            //var x = 1;
            return Tuple.Create(travel, groceryStores);



        }
    
    }
}


//https://maps.googleapis.com/maps/api/distancematrix/json?origins=place_id:ChIJ53qpLDJD04kR7KjCIsq7cTc&destinations=place_id:ChIJN7B0Fp9D04kRjFOKVQ0oblk%7Cplace_id:ChIJdcUvjjFD04kRq6g4mn5ujvU&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&

















//43.1103481, -79.0789613&radius=20000&keyword=Grocery store&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&
//https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=43.1103481,%20-79.0789613&rankby=distance&keyword=Grocery%20store&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&


//https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=43.1105654,%20-79.0797108&rankBy=distance&keyword=Grocery%20store&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&"

//https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=43.1105654,%20-79.0797108&rankby=distance&keyword=Grocery%20store&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&


//https://maps.googleapis.com/maps/api/place/nearbysearch/43.1105654,%20-79.0797108&rankby=distance&keyword=Grocery%20store&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&


//{ https://maps.googleapis.com/maps/api/place/nearbysearch/43.1105654, -79.0797108&rankby=distance&keyword=Grocery store&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&}

//Distance Matrix

/*https://maps.googleapis.com/maps/api/distancematrix/json? destinations = place_id=ChIJN7B0Fp9D04kRjFOKVQ0oblk place_id:ChIJdcUvjjFD04kRq6g4mn5ujvU origins=place_id=ChIJ53qpLDJD04kR7KjCIsq7cTc&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&*/



/*new Address
{
    PlaceID = "ChIJ53qpLDJD04kR7KjCIsq7cTc",
    FullAddress = "4380 Fourth Ave",
    City = "Niagara Falls",
    PostalCode = "L2E 4N2",
    Latitude = 43.1102633,
    Longitude = -79.0785068,
},*/

//GROW
/*new Address
{
    PlaceID = "ChIJN7B0Fp9D04kRjFOKVQ0oblk",
    FullAddress = "GROW Community Food Literacy Centre",
    City = "Niagara Falls",
    PostalCode = "L2E 4N1",
    Latitude = "43.1103374",
    Longitude = "-79.07902519999999",*/


//curl -L -X GET 'https://maps.googleapis.com/maps/api/distancematrix/json?origins=place_id:ChIJ53qpLDJD04kR7KjCIsq7cTc&destinations=place_id:ChIJN7B0Fp9D04kRjFOKVQ0oblk%7Cplace_id:ChIJdcUvjjFD04kRq6g4mn5ujvU&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&'