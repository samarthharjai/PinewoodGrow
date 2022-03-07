using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
    public class Invoice
    {

        public int ID { get; set; }

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

        [ForeignKey("Receipts")]
        //[Required(ErrorMessage = "You must select a Patient.")]
        //[Display(Name = "Patient")]
        public int ReceiptID { get; set; }

        public Receipt Receipts { get; set; }
    }
}
