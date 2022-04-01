using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models
{
	public class Situation : Auditable
	{
        public Situation()
        {
            MemberSituations = new HashSet<MemberSituation>();
        }
        public int ID { get; set; }

        [Display(Name = "Situation")]
        [Required(ErrorMessage = "You cannot leave the Situation blank.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        public string Name { get; set; }

        public ICollection<MemberSituation> MemberSituations { get; set; }
    }
}
