using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class blogEntryVM
    {
        public blogEntryVM(int id, string author, int authorId, string title, string content, DateTime creationDate)
        {
            this.id = id;
            this.author = author;
            this.authorId = authorId;
            this.blogEntryContent = content;
            this.blogEntryTitle = title;
            this.creationDate = creationDate;

            tags = new List<tagsVM>();            
        }

        public blogEntryVM()
        {

        }

        public void PopulateTags(List<blogTag> blogTags)
        {
            foreach(blogTag tag in blogTags)
            {
                tags.Add(new tagsVM(tag.id, tag.tag));
            }
        }

        public int id { get; set; }
        public string author { get; set; }
        public int authorId { get; set; }
        [DisplayName("Content")]
        [Required]
        public string blogEntryContent { get; set; }
        [DisplayName("Title")]
        [Required]
        public string blogEntryTitle { get; set; }
        public System.DateTime creationDate { get; set; }

        public List<tagsVM> tags { get; set; }
        [DisplayName("Tags")]
        public string stringTags { get; set; }

    }
}