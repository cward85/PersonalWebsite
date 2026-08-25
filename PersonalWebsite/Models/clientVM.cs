using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class clientVM
    {
        public int id { get; set; }
        public string ClientName { get; set; }
        public int TotalInvoices { get; set; }
        public float TotalDollarAmount { get; set; }
        public DateTime? DateLastInvoiceSent { get; set; }
    }   
}       
