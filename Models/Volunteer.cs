using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
    public class Volunteer
    {
        public Volunteer()
        {
            Members = new HashSet<Member>();
        }
        public int ID { get; set; }

        public string Name => FirstName + " " + LastName;

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

        public ICollection<Member> Members { get; set; }
    }
}
