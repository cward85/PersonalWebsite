using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class invoiceEntryVM
    {
        public int invoiceEntryId { get; set; }
        public string Description { get; set; }
        public float Rate { get; set; }
        public string RateUnit { get; set; }
        public float UnitAmount { get; set; }
        public float TotalAmount { get; set; }
    }
}