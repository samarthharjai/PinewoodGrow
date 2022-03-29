using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models.Temp
{
    public class TempMember
    {
		public TempMember()
		{
			MemberSituations = new HashSet<TempMemberSituation>();
			MemberDietaries = new HashSet<TempMemberDietary>();
			MemberIllnesses = new HashSet<TempMemberIllness>();
			MemberDocuments = new HashSet<TempMemberDocument>();
			MemberHouseholds = new HashSet<TempMemberHousehold>();
		}


		public string FullName => FirstName + " " + LastName;




	

		public int ID { get; set; }


		public string FirstName { get; set; }


		public string LastName { get; set; }

		/*[Required(ErrorMessage = "You cannot leave the Age blank.")]
		[Range(1, 115, ErrorMessage = "Age must be greater than 0.")]
		public int Age { get; set; }*/


		public DateTime? DOB { get; set; }


		public string Telephone { get; set; }

		public string Email { get; set; }


		public double Income { get; set; }


        public string Notes { get; set; }

		public bool Consent { get; set; }

	
		public int? VolunteerID { get; set; }
		public Volunteer Volunteer { get; set; }

	
		public DateTime CompletedOn { get; set; }


		public int? TempHouseholdID { get; set; }
		public TempHousehold TempHousehold { get; set; }
        public int? HouseholdID { get; set; }
        public Household Household { get; set; }


		public int? GenderID { get; set; }
		public Gender Gender { get; set; }

		public ICollection<TempMemberSituation> MemberSituations { get; set; }
        public ICollection<TempMemberDietary> MemberDietaries { get; set; }

		public ICollection<TempMemberIllness> MemberIllnesses { get; set; }
        public ICollection<TempMemberDocument> MemberDocuments { get; set; }

		public ICollection<TempMemberHousehold> MemberHouseholds { get; set; }
	}
}

