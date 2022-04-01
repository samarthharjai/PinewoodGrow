using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models
{
	public class Dietary : Auditable
	{
        public Dietary()
        {
            MemberDietaries = new HashSet<MemberDietary>();
        }
        public int ID { get; set; }

        [Display(Name = "Dietary Restriction")]
        [Required(ErrorMessage = "You cannot leave the Dietary Restriction blank.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        public string Name { get; set; }

        public ICollection<MemberDietary> MemberDietaries { get; set; }
    }
}
