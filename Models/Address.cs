using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class Address
	{
        public Address()
        {
            Members = new HashSet<Member>();
        }
        public int ID { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "You cannot leave the Address blank.")]
        [StringLength(150, ErrorMessage = "Name cannot be more than 150 characters long.")]
        public string FullAddress { get; set; }

        [Required(ErrorMessage = "You cannot leave the City blank.")]
        [StringLength(100, ErrorMessage = "City cannot be more than 100 characters long.")]
        public string City { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "You cannot leave the Postal Code blank.")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

		public double? Latitude { get; set; }

		public double? Longitude { get; set; }

		public ICollection<Member> Members { get; set; }
    }
}
