using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models
{
    public class Dependant : Auditable

    {
    public int ID { get; set; }

    public DateTime DOB { get; set; }

    public int HouseholdID { get; set; }

    public Household Household { get; set; }
    }
}
