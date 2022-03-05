using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models;

namespace PinewoodGrow.ViewModels
{
    public class StoreMapMarker
    {
        public string Name { get; set; }
        public string FullAddress { get; set; }

        public int MemberCount { get; set; }

        public int HouseholdCount { get; set; }

        public double Lat { get; set; }
        public double Lng { get; set; }

        public StoreMapMarker(GroceryStore store, List<Household> households)
        {
            Name = store.Name;
            FullAddress = store.FullAddress;
            MemberCount = households.Sum(a=> a.FamilySize);
            HouseholdCount = households.Count;
            Lat = store.Latitude;
            Lng = store.Longitude;
        }
    }
}
