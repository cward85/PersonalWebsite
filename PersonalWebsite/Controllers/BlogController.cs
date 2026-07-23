using PersonalWebsite.DAL;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet]
        public ActionResult Index(int pageNumber = 1)
        {
            ViewBag.MetaDescription = "Personal Blog of Camilo Ward where he posts about various web dev and game dev topics.";
            ViewBag.MetaKeywords = "Camilo Ward, Blog, Web Dev, Game Dev";

            DALBlog blog = new DALBlog();

            blogVM model = new blogVM(); 
            model.pageNumber = pageNumber;
            model.blogEntries = blog.ListBlogEntries(pageNumber);
            model.searchByTag = null;
            model.searchByAuthor = null;
            model.searchByAuthorId = 0;
            model.numPages = (blog.CountBlogEntries() / 6) + 1;
            model.newBlogEntry = new blogEntryVM();

            return View(model);
        }

        [HttpGet]
        public ActionResult SearchByTag(string tags, int pageNumber = 1)
        {
            ViewBag.MetaDescription = "Personal Blog of Camilo Ward where he posts about various web dev and game dev topics.";
            ViewBag.MetaKeywords = "Camilo Ward, Blog, Web Dev, Game Dev";
            DALBlog blog = new DALBlog();

            blogVM model = new blogVM();
            model.pageNumber = pageNumber;
            model.blogEntries = blog.ListBlogEntriesByTag(tags, pageNumber);
            model.searchByTag = tags;
            model.searchByAuthor = null;
            model.searchByAuthorId = 0;
            model.numPages = (blog.CountBlogEntriesByTag(tags) / 6) + 1;
            model.newBlogEntry = new blogEntryVM();

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult SearchByAuthor(int authorId, int pageNumber = 1)
        {
            ViewBag.MetaDescription = "Personal Blog of Camilo Ward where he posts about various web dev and game dev topics.";
            ViewBag.MetaKeywords = "Camilo Ward, Blog, Web Dev, Game Dev";
            DALBlog blog = new DALBlog();

            blogVM model = new blogVM();
            model.pageNumber = pageNumber;
            model.blogEntries = blog.ListBlogEntriesByAuthor(authorId, pageNumber);
            model.searchByAuthor = blog.GetAuthorById(authorId);
            model.searchByAuthorId = authorId;
            model.searchByTag = null;
            model.numPages = (blog.CountBlogEntriesByAuthor(authorId) / 6) + 1;
            model.newBlogEntry = new blogEntryVM();

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult AddBlogEntry(blogVM model)
        {
            if (Session["UserId"] != null && (int)Session["UserId"] > 0)
            {
                if (ModelState.IsValid)
                {
                    DALBlog blog = new DALBlog();
                    model.newBlogEntry.authorId = (int)Session["UserId"];                    
                    blog.CreateBlogEntry(model.newBlogEntry);

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (Session["UserId"] != null && (int)Session["UserId"] > 0)
            {                
                DALBlog blog = new DALBlog();

                if (blog.Delete(id, (int)Session["UserId"], (bool)Session["IsAdmin"]))
                {
                    return this.Json(new { message = "Success" });
                }
                else
                {
                    return this.Json(new { message = "Failure" });
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}