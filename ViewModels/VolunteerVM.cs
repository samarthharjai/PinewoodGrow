using PinewoodGrow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.ViewModels
{
	public class VolunteerVM
	{
        public VolunteerVM()
        {
            Members = new HashSet<Member>();
            Active = true;
        }
        public int ID { get; set; }

        [Display(Name = "Volunteer")]
        public string FullName => FirstName + " " + LastName;

        [Display(Name = "Phone")]
        public string PhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(Phone))
                {
                    return "";
                }
                else
                {
                    return "(" + Phone.Substring(0, 3) + ") " + Phone.Substring(3, 3) + "-" + Phone.Substring(6, 4);
                }
            }
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the First Name blank.")]
        [StringLength(50, ErrorMessage = "First Name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the Last Name blank.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be more than 50 characters long.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You cannot leave the Email blank.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string Phone { get; set; }

        public bool Active { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}
