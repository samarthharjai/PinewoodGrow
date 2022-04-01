using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models
{
	public class Household : Auditable
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
		public double HouseIncome => Members.Select(a=> a.Income).ToList().Sum();




		public string HouseSummary =>  ID + " - " + FamilyName;


		[Display(Name = "Family Size")]
        public int FamilySize {
            get => Members.Count  + Dependants;
            set{} }

		[Display(Name = "Family Name")]
		[Required(ErrorMessage = "You cannot leave the Family Name blank.")]
		public string FamilyName { get; set; }

		[Range(-1, 115, ErrorMessage = "Dependants cannot be less than 0.")]
		public int Dependants { get; set; }

		//[Required(ErrorMessage = "You cannot leave the LICO unselected.")]
		public bool LICO {
			get
			{
				{
					bool calcLICO = false;

					switch (FamilySize)
                    {
                        case 1 when HouseIncome <= 22186:
                        case 2 when HouseIncome <= 27619:
                        case 3 when HouseIncome <= 33953:
                        case 4 when HouseIncome <= 41225:
                        case 5 when HouseIncome <= 46757:
                        case 6 when HouseIncome <= 52734:
                        case 7 when HouseIncome <= 58712:
                            calcLICO = true;
                            break;
                    }

					return calcLICO;
				};
			}
        }

        public bool IsFixedAddress { get; set; }

		public bool IsActive { get; set; }


		public int? AddressID { get; set; }
		public Address Address { get; set; }

		public ICollection<Member> Members { get; set; }

		[Display(Name = "Member Households")]
		public ICollection<MemberHousehold> MemberHouseholds { get; set; }
	}
}
