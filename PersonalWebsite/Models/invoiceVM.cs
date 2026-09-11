using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class invoiceVM
    {
        public int id { get; set; }
        public string invoiceNumber { get; set; }
        public DateTime? invoiceDate { get; set; }
        public DateTime? invoiceDueDate { get; set; }
        public DateTime? sentDate { get; set; }
        public float totalAmount { get; set; }
    }
}