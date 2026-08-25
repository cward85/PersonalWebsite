using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Models
{
    public class clientListVM
    {
        public int id { get; set; }
        public List<clientVM> clients { get; set; }

        [DisplayName("Name")]
        [Required]
        [StringLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
        public string name { get; set; }
        [DisplayName("Street 1")]
        [StringLength(120, ErrorMessage = "Street 1 cannot be longer than 120 characters.")]
        [Required]
        public string street1 { get; set; }
        [DisplayName("Street 2")]
        [StringLength(120, ErrorMessage = "Street 2 cannot be longer than 120 characters.")]
        public string street2 { get; set; }
        [DisplayName("City")]
        [StringLength(100, ErrorMessage = "City cannot be longer than 100 characters.")]
        [Required]
        public string city { get; set; }
        [DisplayName("State")]
        public Nullable<int> stateId { get; set; }
        [DisplayName("Country")]
        [Required]
        public int countryId { get; set; }
        [DisplayName("Postal Code")]
        [StringLength(16, ErrorMessage = "Postal Code cannot be longer than 16 characters.")]
        [Required]
        public string postCode { get; set; }

        public IEnumerable<SelectListItem> country { get; set; }

        public IEnumerable<SelectListItem> state { get; set; }

        public string author { get; set; }

        public clientListVM()
        {
            clients = new List<clientVM>();            
        }
    }
}