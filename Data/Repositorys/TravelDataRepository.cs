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
    public class TravelDataRepository
    {
        private static readonly HttpClient client = new HttpClient();

        //Grow Place ID used in Distance Matrix
        internal const string growID = "ChIJN7B0Fp9D04kRjFOKVQ0oblk";


        public TravelDataRepository()
        {
            client.BaseAddress =
                new Uri("https://maps.googleapis.com/maps/api/place/nearbysearch");
        }

        /// <summary>
        /// Get all Travel detail infomation from list of addresses. Returns list of Travel Details, and a List of grocery Stores in a tuple
        /// </summary>
        /// <param name="addresses"> List of addresses you wish to get the travel details off</param>
        /// <param name="stores">Optional Inital list of stores, </param>
        /// <returns></returns>
        public  static async Task<Tuple<List<TravelDetail>, List<GroceryStore>>> GetAllTravelDetails(List<Address> addresses, List<GroceryStore> stores = null)
        {
            var details = new List<TravelDetail>();
            stores ??= new List<GroceryStore>();
            foreach (var address in addresses)
            {
                var (travel, store) = await GetTravelTimes(address);

                details.Add(travel);
                if (stores.All(a => a.ID != store.ID)) stores.Add(store);
            }

            return Tuple.Create(details, stores);
        }

        /// <summary>
        /// Gets travel information for a specific address, returns tuple, travel detail and grocery store
        /// </summary>
        /// <param name="address">Specific address used to get travel details</param>
        /// <returns></returns>
        public  static async Task<Tuple<TravelDetail, GroceryStore>> GetTravelTimes(Address address)
        {
            //Gets rough estimate of closest grocery stores based on distance of a direct line from address to selected location
            var request = $"/maps/api/place/nearbysearch/json?location={address.Latitude},%20{address.Longitude}&rankby=distance&keyword=supermarket%20store&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&";
            var response = await client.GetAsync(request);
            if (!response.IsSuccessStatusCode) throw new Exception("Could not access the Nearest Grocery Stores");

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JsonConvert.DeserializeObject<object>(jsonString));

            var root = await response.Content.ReadAsAsync<Root>();


            //The two closest stores returned from the api
            var possibleStores = new List<GroceryStore>
            {
                GetStore(root.results[0]),
                GetStore(root.results[1]),

            };
            //Evaluates actual travel times of two closest stores to equate the closest store based on durataion rather than distance
            //Additionally Completes first Distance matrix call, getting both the distance and driving duration for Grow, and Grocery store
            var (travel, grocery) = await ConfirmNearestStore(possibleStores, address);

            string[] travelTypes = { "bicycling", "walking" };

            //Gets travel duration for biking and walking
            foreach (var type in travelTypes)
            {
                var Distancerequest = $"/maps/api/distancematrix/json?origins=place_id:{address.PlaceID}&destinations=place_id:{growID}%7Cplace_id:{travel.GroceryID}&mode={type}&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&";
                var Distanceresponse = await client.GetAsync(Distancerequest);
                if (!Distanceresponse.IsSuccessStatusCode) throw new Exception("Could not Get Distance matrix");
                var values = JObject.Parse(await Distanceresponse.Content.ReadAsStringAsync());

                switch (type)
                {
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
            return Tuple.Create(travel, grocery);
        }
        /// <summary>
        /// Converts result from api call to Grocery store object
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        internal static GroceryStore GetStore(Result result)
        {
            return new GroceryStore
            {
                ID = result.place_id,
                Name = result.name,
                FullAddress = result.vicinity,
                Latitude = result.geometry.location.lat,
                Longitude = result.geometry.location.lng
            };
        }
        /// <summary>
        /// Calculates driving duration between household and its two nearest grocery stores to deterimin the closest store based on duration.
        /// Returns the closest Grocery store and Traveldetail containing both the grocery store and grows travel distance and driving duration 
        /// </summary>
        /// <param name="possibleStores">List of 2 closest Stores</param>
        /// <param name="address">Address origin point of distance calculation</param>
        /// <returns></returns>
        internal static async Task<Tuple<TravelDetail, GroceryStore>> ConfirmNearestStore(List<GroceryStore> possibleStores, Address address)
        {
            var Distancerequest = $"/maps/api/distancematrix/json?origins=place_id:{address.PlaceID}&destinations=place_id:{growID}%7Cplace_id:{possibleStores[0].ID}%7Cplace_id:{possibleStores[1].ID}&mode=driving&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&";
            var Distanceresponse = await client.GetAsync(Distancerequest);
            if (!Distanceresponse.IsSuccessStatusCode) throw new Exception("Could not Get Distance matrix");
            var values = JObject.Parse(await Distanceresponse.Content.ReadAsStringAsync());

            var element = 1;


            if (((double)values["rows"][0]["elements"][1]["duration"]["value"]) >
                ((double)values["rows"][0]["elements"][2]["duration"]["value"]))
            {
                element = 2;    //if second store duration is lesser, use it
            }

            var travel = new TravelDetail
            {
                GroceryID = possibleStores[element - 1].ID,
                AddressID = address.ID,
                GrowDrive = ((double)values["rows"][0]["elements"][0]["duration"]["value"]),
                GrowDistance = ((double)values["rows"][0]["elements"][0]["distance"]["value"]),
                GroceryDrive = ((double)values["rows"][0]["elements"][element]["duration"]["value"]),
                GroceryDistance = ((double)values["rows"][0]["elements"][element]["distance"]["value"]),
            };

            var groceryStore = possibleStores[element - 1];
            return Tuple.Create(travel, groceryStore);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
//	PlaceID ChIJbf6m4jFD04kR1yYC_1dzufs
// 	Latitude 43.1090012
// 	Longitude -79.0757509


//Your everything Store  , Closet Place Id ChIJQ5LXLi9D04kRXMX3xTAM3FM
// Lococo's (Victoria) Vegetables and Meats, second place ID ChIJm9VTOy1D04kRklz9MMK3bek"

// https://maps.googleapis.com//maps/api/place/nearbysearch/json?location=43.1090012,%20-79.0757509&rankby=distance&keyword=supermarket%20store&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&"

//https://maps.googleapis.com/maps/api/distancematrix/json?origins=place_id:ChIJbf6m4jFD04kR1yYC_1dzufs&destinations=place_id:ChIJN7B0Fp9D04kRjFOKVQ0oblk%7Cplace_id:ChIJQ5LXLi9D04kRXMX3xTAM3FM%7Cplace_id:ChIJm9VTOy1D04kRklz9MMK3bek&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&


// https://maps.googleapis.com//maps/api/place/nearbysearch/json?location=42.96808,%20-79.18249999999999&rankby=distance&keyword=supermarket%20store&key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&




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