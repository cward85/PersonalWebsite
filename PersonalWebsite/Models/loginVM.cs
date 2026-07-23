using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class loginVM
    {
        [DisplayName("User Name")]
        [Required]
        public string userName { get; set; }
        [DisplayName("Password")]
        [Required]
        public string password { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}