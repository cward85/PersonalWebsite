using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class EmailSubscribers
    {
        public List<string> emailsToSend { get; set; }
        public string blogTitle { get; set; }
        public string blogAuthor { get; set; }
        public DateTime blogDate { get; set; } 

        public EmailSubscribers(List<string> emailList, string title, string author, DateTime date)
        {
            emailsToSend  = emailList;
            blogTitle = title;
            blogAuthor = author;
            blogDate = date;
        }
    }
}