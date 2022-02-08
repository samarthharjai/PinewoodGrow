using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class MemberDietary
	{
		public int DietaryID { get; set; }
		public Dietary Dietary { get; set; }

		public int MemberID { get; set; }
		public Member Member { get; set; }
	}
}
