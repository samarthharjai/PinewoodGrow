using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
    public class TravelDetail
    {
        public int ID { get; set; }

        public double GroceryDistance { get; set; }
        public double GroceryDrive { get;  set; }
        public double GroceryBike { get;  set; }
        public double GroceryWalk { get;  set; }

        public double GrowDistance { get; set; }
        public double GrowDrive { get; set; }
        public double GrowBike { get; set; }
        public double GrowWalk { get; set; }



        public string GroceryID { get; set; }
        public GroceryStore GroceryStore { get; set; }

        public int AddressID { get;  set; }
        public Address Address { get; set; }
  
   

    }

    
}
