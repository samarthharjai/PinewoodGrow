using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
	public class Sale
	{
        public Sale()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }
        public int ID { get; set; }

        [Display(Name = "Transaction Date")]
        [Required(ErrorMessage = "You cannot leave the Transaction Date blank.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TransDate { get; set; }


        [Display(Name = "Net Total")]
        [Required(ErrorMessage = "You cannot leave the Net Total blank.")]
        [DataType(DataType.Currency)]
        [Range(0.00, 9999.99, ErrorMessage = "Net Total must be between $0 and $9,999.99")]
        public double NetTotal { get; set; }

        public int MemberID { get; set; }
        public Member Member { get; set; }
        public int VolunteerID { get; set; }
        public Volunteer Volunteer { get; set; }
        public int PaymentID { get; set; }
        public Payment Payment { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
