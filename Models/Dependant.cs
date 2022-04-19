using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models
{
    public class Dependant : Auditable

    {
        public string Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int? a = today.Year - DOB.Year
                                    - ((today.Month < DOB.Month || (today.Month == DOB.Month && today.Day < DOB.Day) ? 1 : 0));
                return a?.ToString(); /*Note: You could add .PadLeft(3) but spaces disappear in a web page. */
            }
        }

        public bool isValid
        {
            get
            {

                DateTime today = DateTime.Today;
                int? a = today.Year - DOB.Year - ((today.Month < DOB.Month || (today.Month == DOB.Month && today.Day < DOB.Day) ? 1 : 0));
                return a > 0 && a <= 18; /*Note: You could add .PadLeft(3) but spaces disappear in a web page. */

            }
        }
        public int ID { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

    public int HouseholdID { get; set; }

    public Household Household { get; set; }
    }
}
