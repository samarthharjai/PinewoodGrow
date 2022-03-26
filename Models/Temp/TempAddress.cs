using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models.Temp
{
    public class TempAddress
    {
   

        public string FormalAddress => FullAddress + ", " + City + ", " + PostalCode;
        public int ID { get; set; }

        public string FullAddress { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string PlaceID { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public TempHousehold Household { get; set; }

    }
}
