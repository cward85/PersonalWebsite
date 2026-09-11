using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class invoiceEntryListVM
    {
        public List<invoiceEntryVM> invoiceEntries { get; set; }     
        public string invoiceNumber { get; set; }
        public int invoiceEntryId { get; set; }
        public int invoiceId { get; set; }

        [DisplayName("Description")]
        [Required]
        [StringLength(100, ErrorMessage = "Description cannot be more that 100 characters")]
        public string description { get; set; }
        [DisplayName("$ Rate")]
        [Required]
        public float? rate { get; set; }
        [DisplayName("Unit")]
        [Required]
        [StringLength(50, ErrorMessage = "Unit cannot be more than 50 characters")]
        public string rateUnit { get; set; }
        [DisplayName("Amount")]
        [Required]
        public float? unitAmount { get; set; }
        
        public invoiceEntryListVM()
        {
            invoiceEntries = new List<invoiceEntryVM>();
        }
    }
}