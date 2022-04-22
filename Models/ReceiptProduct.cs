using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Models
{
    public class ReceiptProduct
    {
        public int ID { get; set; }
        public int ReceiptID { get; set; }
        public Receipt Receipt { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Total => Quantity * UnitPrice;
    }
}
