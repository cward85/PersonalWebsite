using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class invoiceListVM
    {
        public string ClientName { get; set; }
        public List<invoiceVM> invoiceEntries { get; set; }

        [DisplayName("Invoice Date")]
        public DateTime invoiceDate { get; set; }
        [DisplayName("Invoice Due Date")]
        public DateTime invoiceDueDate { get; set; }
       
        public int clientId { get; set; }
        public invoiceListVM() 
        {
            invoiceEntries = new List<invoiceVM>();
        }
    }
}