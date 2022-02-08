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
			MemberHouseholds = new HashSet<MemberHousehold>();
		}

		[Display(Name = "Member #")]
		public int ID { get; set; }

		[Display(Name = "Household Income")]
		//[Required(ErrorMessage = "You cannot leave the House Income blank.")]
		[DataType(DataType.Currency)]
		[Range(0.0, 58712, ErrorMessage = "Income must be between $0 and $58,712.")]
		public double HouseIncome {
			get 
			{
				{
					double income = 0;

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

		//[Required(ErrorMessage = "You cannot leave the LICO unselected.")]
		public bool LICO {
			get
			{
				{
					bool calcLICO = false;

					if (FamilySize == 1 && HouseIncome <= 22186)
						calcLICO = true;
					else if (FamilySize == 2 && HouseIncome <= 27619)
						calcLICO = true;
					else if (FamilySize == 3 && HouseIncome <= 33953)
						calcLICO = true;
					else if (FamilySize == 4 && HouseIncome <= 41225)
						calcLICO = true;
					else if (FamilySize == 5 && HouseIncome <= 46757)
						calcLICO = true;
					else if (FamilySize == 6 && HouseIncome <= 52734)
						calcLICO = true;
					else if (FamilySize == 7 && HouseIncome <= 58712)
						calcLICO = true;

					return calcLICO;
				};
			}
			set
			{

			}
		}

		public int AddressID { get; set; }
		public Address Address { get; set; }

		public ICollection<Member> Members { get; set; }

		[Display(Name = "Member Households")]
		public ICollection<MemberHousehold> MemberHouseholds { get; set; }
	}
}
