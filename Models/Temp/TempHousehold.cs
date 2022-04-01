using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models.Temp
{
    public class TempHousehold : Auditable 
    {
		public TempHousehold()
		{
			Members = new HashSet<TempMember>();
			MemberHouseholds = new HashSet<TempMemberHousehold>();
		}

        public DateTime TimeCreated { get; set; }
		public int ID { get; set; }

		public double HouseIncome => Members.Select(a => a.Income).ToList().Sum();


		public string HouseSummary => ID + " - " + FamilyName;



		public int FamilySize
		{
			get => Members.Count + Dependants;
			set { }
		}


		public string FamilyName { get; set; }


		public int Dependants { get; set; }

		//[Required(ErrorMessage = "You cannot leave the LICO unselected.")]
		public bool LICO
		{
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
		public TempAddress Address { get; set; }

		public ICollection<TempMember> Members { get; set; }

		public ICollection<TempMemberHousehold> MemberHouseholds { get; set; }
	}
}

