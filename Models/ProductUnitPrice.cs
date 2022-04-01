using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models
{
    public class ProductUnitPrice : Auditable
    {
        public ProductUnitPrice()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int ID { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "You cannot leave the name of the city blank.")]
        //[StringLength(255, ErrorMessage = "City name cannot be more than 255 characters long.")]
        [DataType(DataType.Currency)]
        public double ProductPrice { get; set; }

        [Required(ErrorMessage = "You must select the Product.")]
        [Display(Name = "Products")]
        public int ProductID { get; set; }

        public DateTime Date { get; set; }
        public Product Product { get; set; }


        public ICollection<Invoice> Invoices { get; set; }
    }
}
