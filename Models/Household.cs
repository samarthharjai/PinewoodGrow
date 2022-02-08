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

		[Display(Name = "Household #")]
		public int ID { get; set; }

		[Display(Name = "Household Income")]
		[Required(ErrorMessage = "You cannot leave the House Income blank.")]
		[DataType(DataType.Currency)]
		[Range(0.0, 58712, ErrorMessage = "Income must be between $0 and $58,712.")]
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
            set
            {

            }
        }

		[Display(Name = "Family Size")]
		[Required(ErrorMessage = "You cannot leave the Family Size blank.")]
		[Range(1, 115, ErrorMessage = "Family Size must be greater than 0.")]
		public int FamilySize { get; set; }

		[Range(-1, 115, ErrorMessage = "Dependants cannot be less than 0.")]
		public int Dependants { get; set; }

		[Required(ErrorMessage = "You cannot leave the LICO unselected.")]
		public bool LICO { get; set; }

		public int AddressID { get; set; }
		public Address Address { get; set; }

		public ICollection<Member> Members { get; set; }
	}
}
