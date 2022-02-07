using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
    public class MemberHousehold
    {
        public int MemberID { get; set; }
        public Member Member { get; set; }

        public int HouseholdID { get; set; }
        public Household Household { get; set; }
    }
}
