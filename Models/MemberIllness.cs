using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class MemberIllness
	{
  
		public int IllnessID { get; set; }
		public Illness Illness { get; set; }

		public int MemberID { get; set; }
		public Member Member { get; set; }
	}
}
