using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class blogVM
    {
        public List<blogEntryVM> blogEntries { get; set; }
        public int pageNumber { get; set; }
        public string searchByTag { get; set; }
        public string searchByAuthor { get; set; }
        public int searchByAuthorId { get; set; }
        public int numPages { get; set; }
        public blogEntryVM newBlogEntry { get; set; }
    }
}