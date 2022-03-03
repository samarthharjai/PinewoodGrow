using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class Payment
	{
        public Payment()
        {
            Sales = new HashSet<Sale>();
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave the Type blank.")]
        [StringLength(50, ErrorMessage = "Type cannot be more than 50 characters long.")]
        public string Type { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
