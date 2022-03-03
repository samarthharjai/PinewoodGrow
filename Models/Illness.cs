using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class Illness
	{
        public Illness()
        {
            MemberIllnesses = new HashSet<MemberIllness>();
        }
        public int ID { get; set; }

        [Display(Name = "Illness")]
        [Required(ErrorMessage = "You cannot leave the Illness blank.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        public string Name { get; set; }

        public ICollection<MemberIllness> MemberIllnesses { get; set; }
    }
}
