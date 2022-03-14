using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class MemberSituation
	{
		public int ID { get; set; }

		public int SituationID { get; set; }
		public Situation Situation { get; set; }

		public int MemberID { get; set; }
		public Member Member { get; set; }

		[Display(Name = "Member Situation")]
		public string Summary
		{
			get
			{
				return Situation?.Name + ": " + SituationIncome.ToString("c");
			}
		}

		[Required(ErrorMessage = "You must enter the amount of the Income Earned")]
		[DataType(DataType.Currency)]
		[Range(0d, 22186d, ErrorMessage = "Employed Income must be between $0 and $22,186.")]
		public double SituationIncome { get; set; }
	}
}
