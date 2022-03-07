using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models;

namespace PinewoodGrow.ViewModels
{
    public class MapMarker
    {
        public int ID { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
        public double Income { get; set; }
        public string FamilyName { get; set; }
        public string Color { get; set; }

        public double IncomeBracket { get; set; }
        public int FamilySize { get; set; }

        public string Category { get; set; }

        public string GroceryName { get; set; }
        public double GroceryDistance { get; set; }
        public double GroceryDrive { get; set; }
        public double GroceryBike { get; set; }
        public double GroceryWalk { get; set; }

        public double GrowDistance { get; set; }
        public double GrowDrive { get; set; }
        public double GrowBike { get; set; }
        public double GrowWalk { get; set; }

        public MapMarker(Household a, double income, int memberCount)
        {
            IncomeBracket = GetIncomePercentage(memberCount, income);
            ID = a.ID;
            (Icon, Color, Category) = GetColor(IncomeBracket);
            Address = a.Address.FullAddress;
            if (a.Address.Latitude != null) Lat = (double)a.Address.Latitude;
            if (a.Address.Longitude != null) Lng = (double)a.Address.Longitude;
            Income = income;
            FamilySize = a.FamilySize;
            FamilyName = a.FamilyName;
            GrowDistance = a.Address.TravelDetail.GrowDistance;
            GrowDrive = a.Address.TravelDetail.GrowDrive;
            GrowBike = a.Address.TravelDetail.GrowBike;
            GrowWalk = a.Address.TravelDetail.GrowWalk;
            GroceryName = a.Address.TravelDetail.GroceryStore.Name;
            GroceryDistance = a.Address.TravelDetail.GroceryDistance;
            GroceryDrive = a.Address.TravelDetail.GroceryDrive;
            GroceryBike = a.Address.TravelDetail.GroceryBike;
            GroceryWalk = a.Address.TravelDetail.GroceryWalk;
        }

        internal double GetIncomePercentage(int size, double income)
        {
            var upper = size switch
            {
                1 => 22186,
                2 => 27619,
                3 => 33953,
                4 => 41225,
                5 => 46757,
                6 => 52734,
                _ => 58712
            };

            return (income / upper) * 100;

        }
        internal Tuple<string, string, string> GetColor(double percentage)
        {
            return percentage switch
            {
                var i when i <= 30 => Tuple.Create("http://maps.google.com/mapfiles/ms/icons/red-dot.png", "#fd7567", "Lowest"),
                var i when i <= 50 => Tuple.Create("http://maps.google.com/mapfiles/ms/icons/yellow-dot.png", "#fdf569", "Low"),
                var i when i <= 70 => Tuple.Create("http://maps.google.com/mapfiles/ms/icons/purple-dot.png", "#8e67fd", "Mid"),
                var i when i <= 90 => Tuple.Create("http://maps.google.com/mapfiles/ms/icons/blue-dot.png", "#6a92fc", "High"),
                _ => Tuple.Create("http://maps.google.com/mapfiles/ms/icons/green-dot.png", "#00e64d", "Top"),

            };
        }



        /*
        internal Tuple<string, string, string> GetColor(double income)
        {
            return income switch
            {
                var i when i <= 5000 => Tuple.Create("http://maps.google.com/mapfiles/ms/icons/blue-dot.png", "#6a92fc", "Lowest"),
                var i when i <= 10000 => Tuple.Create("http://maps.google.com/mapfiles/ms/icons/red-dot.png", "#fd7567", "Low"),
                var i when i <= 15000 => Tuple.Create("http://maps.google.com/mapfiles/ms/icons/purple-dot.png", "#8e67fd", "Mid"),
                var i when i <= 20000 => Tuple.Create("http://maps.google.com/mapfiles/ms/icons/yellow-dot.png", "#fdf569", "High"),
                _ => Tuple.Create("http://maps.google.com/mapfiles/ms/icons/green-dot.png", "#00e64d", "Top"),

            };
        }*/
    }
}
