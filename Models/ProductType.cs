using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models
{
	public class ProductType : Auditable
	{
		public ProductType()
		{
			Products = new HashSet<Product>();
		}

		public int ID { get; set; }

		[Display(Name = "Product Type")]
		[Required(ErrorMessage = "You cannot leave the Product Type blank.")]
		[StringLength(99, ErrorMessage = "Product cannot be more than 99 characters long.")]
		public string Type { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
