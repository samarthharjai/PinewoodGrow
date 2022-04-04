using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models
{
    public class Receipt : Auditable
    {
        /*public Receipt()
        {
            Invoices = new HashSet<Invoice>();
        }*/

        public int ID { get; set; }

        [Display(Name = "Product Type")]
        [Required(ErrorMessage = "You cannot leave Product Type blank")]
        public int ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }

        [Display(Name = "Products")]
        [Required(ErrorMessage = "You cannot leave Products blank")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "You cannot leave the Quantity blank.")]
        [Range(1, 99, ErrorMessage = "Quantity must be between 1 and 99")]
        public int Quantity { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [Range(0.0, 9999, ErrorMessage = "Income must be between $0 and $9999.")]
        public double Total { get; set; }

        [Display(Name = "SubTotal")]
        [DataType(DataType.Currency)]
        [Range(0.0, 9999, ErrorMessage = "SubTotal must be between $0 and $9999.")]
        public double SubTotal { get; set; }

        [Display(Name = "Unit Total")]
        [Required(ErrorMessage = "Please Select a Payment Method")]
        public int ProductUnitPriceID { get; set; }
        public ProductUnitPrice ProductUnitPrice { get; set; }


        [Display(Name = "Completed On")]
        [Required(ErrorMessage = "You cannot leave the Completed On blank.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CompletedOn { get; set; }

        [Display(Name = "Household")]
        [Required(ErrorMessage = "You cannot leave Household blank")]
        public int HouseholdID { get; set; }
        public Household Household { get; set; }

        [Display(Name = "Volunteer")]
        [Required(ErrorMessage = "You cannot leave the Completed By blank.")]
        public int VolunteerID { get; set; }
        public Volunteer Volunteer { get; set; }

        [Display(Name = "Payment")]
        [Required(ErrorMessage = "Please Select a Payment Method")]
        public int PaymentID { get; set; }
        public Payment Payment { get; set; }

        //public ICollection<Invoice> Invoices { get; set; }
    }
}
