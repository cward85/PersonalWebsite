using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class invoicePDFVM
    {
        public string invoiceNumber { get; set; }
        public string invoiceDate { get; set; }
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public List<invoiceEntry> invoiceEntries { get; set; }
        public string invoiceTotal { get; set; }
        public string invoiceDueDate { get; set; }

    }
}