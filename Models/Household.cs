using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class Household
	{
		public Household()
		{
			Members = new HashSet<Member>();
		}

		[Display(Name = "Household")]
		public int ID { get; set; }

		[Display(Name = "Household Income")]
		[Required(ErrorMessage = "You cannot leave the House Income blank.")]
		[DataType(DataType.Currency)]
		[Range(0.0, 22186, ErrorMessage = "Income must be between $0 and $22,186.")]
		public int HouseIncome { get; set; }

		[Required(ErrorMessage = "You cannot leave the LICO unselected.")]
		public bool LICO { get; set; }

		public ICollection<Member> Members { get; set; }
	}
}
