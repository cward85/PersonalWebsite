using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class tagsVM
    {
        public tagsVM(int id, string tag)
        {
            this.id = id;
            this.tag = tag;
        }

        public int id { get; set; }        
        public string tag { get; set; }
    }
}