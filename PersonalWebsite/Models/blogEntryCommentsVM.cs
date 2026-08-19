using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class blogEntryCommentVM
    {
        public int id { get; set; }
        public Nullable<int> blogId { get; set; }
        public string ipAddress { get; set; }
        public string comment { get; set; }
        public string displayName { get; set; }
        public string emailAddress { get; set; }
        public Nullable<System.DateTime> commentDate { get; set; }        
    }
}