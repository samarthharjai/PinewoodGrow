using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
    public class GroceryStore
    {
        public GroceryStore()
        {
            TravelDetails = new HashSet<TravelDetail>();
        }
  
        public int MemberCount => TravelDetails.Count;
        public string ID { get; set; }

        public string FullAddress { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }


        public ICollection<TravelDetail> TravelDetails { get; set; }

    }
}
