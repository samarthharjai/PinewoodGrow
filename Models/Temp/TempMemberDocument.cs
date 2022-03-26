using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models.Temp
{
    public class TempMemberDocument : UploadedFile
    {
        public int MemberID { get; set; }
        public TempMember Member { get; set; }
    }
}
