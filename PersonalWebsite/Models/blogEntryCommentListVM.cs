using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class blogEntryCommentListVM
    {
        public blogEntryCommentListVM()
        {
            comments = new List<blogEntryCommentVM>();
        }

        public List<blogEntryCommentVM> comments { get; set; }

        [DisplayName("Display Name")]
        [MaxLength(100, ErrorMessage = "Display Name can not be more than 100 characters")]
        [Required]
        public string displayName { get; set; }
        [DisplayName("Comment")]
        [Required]
        public string comment { get; set; }
    }
}