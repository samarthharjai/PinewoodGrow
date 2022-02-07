using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class Member
	{
		public Member()
		{
			MemberSituations = new HashSet<MemberSituation>();
			MemberDietaries = new HashSet<MemberDietary>();
			MemberDocuments = new HashSet<MemberDocument>();
		}

		[Display(Name = "Member")]
		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

		[Display(Name = "Phone")]
		public string TelephoneFormatted
		{
			get
			{
				return "(" + Telephone.Substring(0, 3) + ") " + Telephone.Substring(3, 3) + "-" + Telephone[6..];
			}
		}

		public int ID { get; set; }

		[Display(Name = "First Name")]
		[Required(ErrorMessage = "You cannot leave the First Name blank.")]
		[StringLength(50, ErrorMessage = "First Name cannot be more than 50 characters long.")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		[Required(ErrorMessage = "You cannot leave the Last Name blank.")]
		[StringLength(50, ErrorMessage = "Last Name cannot be more than 50 characters long.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "You cannot leave the Age blank.")]
		[Range(1, 115, ErrorMessage = "Age must be greater than 0.")]
		public int Age { get; set; }

		[Required(ErrorMessage = "You cannot leave the Telephone blank.")]
		[DataType(DataType.PhoneNumber)]
		public string Telephone { get; set; }

		[Required(ErrorMessage = "You cannot leave the Email blank.")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		

		
		[Required(ErrorMessage = "You cannot leave the Income blank.")]
		[DataType(DataType.Currency)]
		[Range(0.0, 22186, ErrorMessage = "Income must be between $0 and $22,186.")]
		public decimal Income { get; set; }

		[StringLength(2000, ErrorMessage = "Member Notes cannot be more than 2000 characters long.")]
		[DataType(DataType.MultilineText)]
		public string Notes { get; set; }

		[Required(ErrorMessage = "You cannot leave the Consent unselected.")]
		public bool Consent { get; set; }

		[Display(Name = "Completed By")]
		[Required(ErrorMessage = "You cannot leave the Completed By blank.")]
		[StringLength(100, ErrorMessage = "Completed By cannot be more than 100 characters long.")]
		public string CompletedBy { get; set; }

		[Display(Name = "Completed On")]
		[Required(ErrorMessage = "You cannot leave the Completed On blank.")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime CompletedOn { get; set; }

		[Display(Name = "Household")]
		[Required(ErrorMessage = "You cannot leave Household blank")]
		public int HouseholdID { get; set; }
		public Household Household { get; set; }

		[Display(Name = "Gender")]
		[Required(ErrorMessage = "You cannot leave Gender blank")]
		public int GenderID { get; set; }
		public Gender Gender { get; set; }

		[Display(Name = "Member Situation")]
		[Required(ErrorMessage = "You cannot leave Member Situation blank")]
		public ICollection<MemberSituation> MemberSituations { get; set; }

		[Display(Name = "Dietary Restrictions")]
		public ICollection<MemberDietary> MemberDietaries { get; set; }

		[Display(Name = "Documents")]
		public ICollection<MemberDocument> MemberDocuments { get; set; }
	}
}
