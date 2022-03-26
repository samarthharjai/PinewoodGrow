using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models.Temp
{
    public class TempMemberHousehold
    {
        public int ID { get; set; }

        public int? MemberID { get; set; }
        public TempMember Member { get; set; }

        public int? HouseholdID { get; set; }
        public TempHousehold Household { get; set; }
    }
}
