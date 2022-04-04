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

    public class IncomeStats
    {
        public string Name { get; set; }
        public double Income { get; set; }
    }

    
    public class PieChartData
    {
        private string[] AvalColors = new []{"#397A4C", "#397A6D", "#39677A", "#39477A", "#4C397A", "#6D397A", "#8F1D0E", "#CE550B", "#7A3967" };

        public List<IncomeStats> IncomeStats { get; set; }

        public string[] Colors { get; set; }

        public PieChartData(List<IncomeStats> incomeStats)
        {

            IncomeStats = incomeStats;
            Colors = GetColors(incomeStats.Count);


        }

        private string[] GetColors(int count)
        {
            var colors = new List<string>();

            colors.Add(AvalColors[0]);

            if (count == 3)
            {
                colors.Add(AvalColors[AvalColors.Length/2]);
            }

            if (count > 3 && count <= 7)
            {
                double multi = (AvalColors.Length - 3) / (count - 2);
                multi = Math.Floor(multi);

                for (var i = multi; i < AvalColors.Length - 3; i+=multi)
                {
                    colors.Add(AvalColors[(int)i]);
                }

            }else if(count > 7 )

            {
                double multi = (AvalColors.Length - 2) / (count - 2);
                multi = Math.Floor(multi);

                for (var i = multi; i < AvalColors.Length; i += multi)
                {
                    colors.Add(AvalColors[(int)i]);
                }
            }
            colors.Add(AvalColors[^1]);
            return colors.ToArray();

        }

    }


}
