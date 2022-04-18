using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models.Audit;

namespace PinewoodGrow.Models
{
    public class LICOInfo : Auditable
    {


        public int ID { get; set; }
        public int HouseholdID { get; set; }
        public bool IsVerified { get; set; }
        public int FamilySize { get; set; }
        [DataType(DataType.Currency)]
        public double Income { get; set; }

        [DataType(DataType.Currency)]
        public double MaxIncome { get; set; }

        public bool IsOverride { get; set; }

        public Household Household { get; set; }


        public int DaysTillNext => (int)(CreatedOn.Value.AddYears(1) - DateTime.Today).TotalDays;

        public void Override()
        {
            if(IsVerified) return;
            IsOverride = true;
            IsVerified = true;


        }

        public void RemoveOverride()
        {
            if(!IsOverride)  return;
            IsOverride = false;
            IsVerified = false;
        }
        public void Verify()
        {
            MaxIncome = FamilySize switch
            {
                1 => 22186,
                2 => 27619,
                3 => 33953,
                4 => 41225,
                5 => 46757,
                6 => 52734,
                _ => 58712,
            };

            if (IsOverride) IsVerified = true;
            else IsVerified = Income <= MaxIncome;

        }

    }
}
