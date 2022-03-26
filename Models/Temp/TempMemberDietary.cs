using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models.Temp
{
    public class TempMemberDietary
    {

        public int DietaryID { get; set; }
        public Dietary Dietary { get; set; }

        public int MemberID { get; set; }
        public TempMember Member { get; set; }
    }
}
