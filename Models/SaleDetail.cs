using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class SaleDetail
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "You cannot leave the Quantity blank.")]
		[Range(1, 999, ErrorMessage = "Quantity must be greater than 0.")]
		public int Quantity { get; set; }

		public int SaleID { get; set; }
		public Sale Sale { get; set; }

		public int ProductID { get; set; }
		public Product Product { get; set; }
	}
}
