﻿using System;
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
			MemberHouseholds = new HashSet<MemberHousehold>();
		}

		[Display(Name = "Household")]
		public int ID { get; set; }

		[Display(Name = "Household Income")]
		[Required(ErrorMessage = "You cannot leave the House Income blank.")]
		[DataType(DataType.Currency)]
		[Range(0.0, 22186, ErrorMessage = "Income must be between $0 and $22,186.")]
		public decimal HouseIncome {
			get 
			{
				{
					decimal income = 0;

					foreach (Member m in Members)
					{
						income += m.Income;
					}

					return income;
				};
			}
			set { }
		}

		[Required(ErrorMessage = "You cannot leave the LICO unselected.")]
		public bool LICO { get; set; }

		[Display(Name = "Dependents")]
		public int Dependents { get; set; }


		public ICollection<Member> Members { get; set; }

		[Display(Name = "Member Households")]
		public ICollection<MemberHousehold> MemberHouseholds { get; set; }
	}
}
