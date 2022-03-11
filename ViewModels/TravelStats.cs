using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models;

namespace PinewoodGrow.ViewModels
{

    public class TravelDataPoints
    {
        public double Income { get; set; }
        public double GrowDistance { get; set; }
        public double GroceryDistance { get; set; }
        public TravelDataPoints( Household household, TravelDetail travel)
        {
            Income = household.HouseIncome;
            GrowDistance = travel.GrowDistance;
            GroceryDistance = travel.GroceryDistance;
        }

    }




    public class TravelStats
    {
        public Avg Grow { get; set; }

        public Avg Store { get; set; }

        public TravelStats(List<TravelDetail> details)
        {
            var len = details.Count;
           var TempGrow = new Avg();
            var TempStore = new Avg();
            foreach (var detail in details)
            {
                TempGrow.Walk += detail.GrowWalk;
                TempGrow.Bike += detail.GrowBike;
                TempGrow.Drive += detail.GrowDrive;
                TempGrow.Distance += detail.GrowDistance;
                TempStore.Walk += detail.GroceryWalk;
                TempStore.Drive += detail.GroceryDrive;
                TempStore.Bike += detail.GroceryBike;
                TempStore.Distance += detail.GroceryDistance;

            }
            Grow = new Avg()
            {
                Walk = Math.Round(TempGrow.Walk / len,2),
                Bike = Math.Round(TempGrow.Bike / len,2),
                Drive = Math.Round(TempGrow.Drive / len,2),
                Distance = Math.Round(TempGrow.Distance / len, 2),
            };

            Store = new Avg()
            {
                Walk = Math.Round(TempStore.Walk / len,2),
                Bike = Math.Round(TempStore.Bike / len,2),
                Drive = Math.Round(TempStore.Drive / len,2),
                Distance = Math.Round(TempStore.Distance / len, 2),
            };
        }
        public TravelStats(TravelDetail detail)
        {
 
    
            Grow = new Avg()
            {
                Walk = detail.GrowWalk,
                Bike = detail.GrowBike,
                Drive = detail.GrowDrive,
                Distance = detail.GrowDistance,
            };

            Store = new Avg()
            {
                Walk = detail.GroceryWalk,
                Bike = detail.GroceryBike,
                Drive = detail.GroceryDrive,
                Distance = detail.GroceryDistance,
            };
        }
        public TravelStats()
        {


            Grow = new Avg()
            {
                Walk = 0,
                Bike = 0,
                Drive = 0,
                Distance = 0,
            };

            Store = new Avg()
            {
                Walk = 0,
                Bike = 0,
                Drive = 0,
                Distance = 0,
            };
        }
    }
 
    public class Avg
    {
        public string DriveFormat => GetMin(Drive);
        public string BikeFormat => GetMin(Bike);
        public string WalkFormat => GetMin(Walk);

        public string DistanceFormat => $"{Math.Round(Distance,0)}M";
        public double Drive { get; set; }
        public double Bike { get; set; }
        public double Walk { get; set; }
        public double Distance { get; set; }

        internal string GetMin(double sec)
        {
            if (sec < 60) return "< 1 min";
            return Math.Round(sec / 60, 0) + " min";

        }
   
    }

}
