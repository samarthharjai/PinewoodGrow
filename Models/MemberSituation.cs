using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class MemberSituation
	{
		public int SituationID { get; set; }
		public Situation Situation { get; set; }

		public int MemberID { get; set; }
		public Member Member { get; set; }
	}
}
