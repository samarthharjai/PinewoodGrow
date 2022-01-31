using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class Gender
	{
        public Gender()
        {
            Members = new HashSet<Member>();
        }
        public int ID { get; set; }

        [Display (Name = "Gender")]
        [Required(ErrorMessage = "You cannot leave the name blank.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        public string Name { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}
