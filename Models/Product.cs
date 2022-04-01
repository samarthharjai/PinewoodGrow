using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models
{
	public class Product : Auditable
    {
        public Product()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }
        public int ID { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "You cannot leave the Product Name blank.")]
        [StringLength(99, ErrorMessage = "Product cannot be more than 99 characters long.")]
        public string Name { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "You cannot leave the Unit Price blank.")]
        [DataType(DataType.Currency)]
        [Range(0.00, 999.99, ErrorMessage = "Unit Price must be between $0 and $999.99")]
        public double UnitPrice { get; set; }

        [Display(Name = "Product Type")]
        [Required(ErrorMessage = "You cannot leave Product Type blank")]
        public int ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }
        public ICollection<ProductUnitPrice> ProductUnitPrices { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
