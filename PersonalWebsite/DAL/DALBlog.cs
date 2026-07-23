using Newtonsoft.Json;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PersonalWebsite.DAL
{
    public class DALBlog
    {
        CWARDEntities _dbContext;

        public DALBlog()
        {
            _dbContext = new CWARDEntities();
        }

        public int CreateBlogEntry(blogEntryVM entryVM)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                blogEntry entry = new blogEntry()
                {
                    authorId = entryVM.authorId,
                    blogEntryContent = entryVM.blogEntryContent,
                    blogEntryTitle = entryVM.blogEntryTitle,
                    creationDate = DateTime.Now,
                    activeFlag = true
                };

                var id = _dbContext.blogEntries.Add(entry);
                _dbContext.SaveChanges();

                var tags = entryVM.stringTags.Split(',');

                for (int i = 0; i < tags.Length; i++)
                {
                    _dbContext.blogTags.Add(new blogTag() { blogEntryId = id.id, tag = tags[i] });
                    _dbContext.SaveChanges();
                }

                transaction.Commit();

                return id.id;
            }
        }

        public bool Delete(int id, int userId, bool isAdmin)
        {
            var entry = _dbContext.blogEntries.Find(id);

            if (entry.authorId == userId || isAdmin)
            {
                entry.activeFlag = false;
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public List<blogEntryVM> ListBlogEntries(int pageNumber)
        {
            var entries = _dbContext.blogEntries.Where(x => x.activeFlag).OrderByDescending(x => x.creationDate).Skip(5 * (pageNumber - 1)).Take(5).ToList();

            return PopulateBlogEntries(entries);
        }

        public List<blogEntryVM> ListBlogEntriesByTag(string tag, int pageNumber)
        {
            var entries = _dbContext.blogEntries.Where(x => x.blogTags.Where(y => y.tag == tag).Any() && x.activeFlag).OrderByDescending(x => x.creationDate).Skip(5 * (pageNumber - 1)).Take(5).ToList();

            return PopulateBlogEntries(entries);
        }

        public List<blogEntryVM> ListBlogEntriesByAuthor(int id, int pageNumber)
        {
            var entries = _dbContext.blogEntries.Where(x => x.authorId == id && x.activeFlag).OrderByDescending(x => x.creationDate).Skip(5 * (pageNumber - 1)).Take(5).ToList();

            return PopulateBlogEntries(entries);
        }        

        public string GetAuthorById(int id)
        {
            return _dbContext.authors.Find(id).name;
        }

        public int CountBlogEntries()
        {
            return _dbContext.blogEntries.Count();
        }

        public int CountBlogEntriesByAuthor(int id)
        {
            return _dbContext.blogEntries.Where(x => x.authorId == id).Count();
        }

        public int CountBlogEntriesByTag(string tag)
        {
            return _dbContext.blogEntries.Where(x => x.blogTags.Where(y => y.tag == tag).Any()).Count();
        }

        private List<blogEntryVM> PopulateBlogEntries(List<blogEntry> entries)
        {
            List<blogEntryVM> entriesVM = new List<blogEntryVM>();

            foreach (blogEntry entry in entries)
            {
                var tags = _dbContext.blogTags.Where(x => x.blogEntryId == entry.id).ToList();
                var newEntry = new blogEntryVM(entry.id, _dbContext.authors.Where(x => x.id == entry.authorId).FirstOrDefault().name, entry.authorId, entry.blogEntryTitle, entry.blogEntryContent.Replace("\\n", Environment.NewLine), entry.creationDate);
                newEntry.PopulateTags(tags);

                entriesVM.Add(newEntry);
            }

            return entriesVM;
        }
    }
}