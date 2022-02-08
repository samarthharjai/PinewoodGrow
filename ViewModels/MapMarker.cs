using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.ViewModels
{
    public class MapMarker
    {
        public int ID { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }

        public double Income { get; set; }

        public string Color { get; set; }

        public int FamilySize { get; set; }
        public string Category { get; set; }
    }
}
