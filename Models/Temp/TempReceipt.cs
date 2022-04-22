using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;
using PinewoodGrow.Models.Temp;

namespace PinewoodGrow.Models.Temp
{
    public class TempReceipt : Auditable
    {
        public TempReceipt()
        {
            Products = new HashSet<TempReceiptProduct>();
        }

        public int ID { get; set; }


        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [Range(0.0, 9999, ErrorMessage = "Income must be between $0 and $9999.")]
        public double Total { get; set; }

        [Display(Name = "SubTotal")]
        [DataType(DataType.Currency)]
        [Range(0.0, 9999, ErrorMessage = "SubTotal must be between $0 and $9999.")]
        public double SubTotal { get; set; }


        [Display(Name = "Completed On")]
        [Required(ErrorMessage = "You cannot leave the Completed On blank.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CompletedOn { get; set; }



        public ICollection<TempReceiptProduct> Products { get; set; }

        //public ICollection<Invoice> Invoices { get; set; }
    }
}
