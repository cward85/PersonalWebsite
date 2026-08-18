using PersonalWebsite.DAL;
using PersonalWebsite.Helpers;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> AddBlogEntry(blogVM model)
        {
            if (Session["UserId"] != null && (int)Session["UserId"] > 0)
            {
                if (ModelState.IsValid)
                {
                    DALBlog blog = new DALBlog();
                    model.newBlogEntry.authorId = (int)Session["UserId"];
                    model.newBlogEntry.creationDate = DateTime.Now;
                    var id = blog.CreateBlogEntry(model.newBlogEntry);
                    if (id > 0)
                    {
                        model.newBlogEntry.id = id;
                        var subscription = blog.GetSubscriberEmails(model.newBlogEntry);
                        string blogName = ConfigurationManager.AppSettings["BlogName"].ToString();
                        await EmailHandler.SendMultipleEmailsAsync(subscription.emailsToSend, blogName + " Blog - New Blog Entry", "New Blog Entry: " + subscription.blogTitle + " by " + subscription.blogAuthor + " - " + subscription.blogDate.ToString());
                    }

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

        [HttpPost]
        public async Task<ActionResult> Subscribe(string emailAddress)
        {
            if (EmailHandler.EmailRegex.IsMatch(emailAddress))
            {
                string passCode = HelperFunctions.GeneratePasscode(20);

                DALBlog dalBlog = new DALBlog();

                if (dalBlog.Subscribe(emailAddress, passCode, Request.UserHostAddress))
                {
                    string blogName = ConfigurationManager.AppSettings["BlogName"].ToString();
                    var request = HttpContext.Request;
                    var url = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, "/Blog/ValidateEmail?code=" + passCode + "&email=" + emailAddress);
                    await EmailHandler.SendEmailAsync(emailAddress, blogName + " Blog - Subscription Validation Email", 
                        "You have subscribed to the " + blogName + " blog.  To complete the process, click the link below to validate your email." +
                        "</br></br>" +
                        "<a href='" + url + "'>" + url + "</a>" +
                        "</br></br>" +
                        "If you did not initiate this subscription, you may ignore this email.  But if you're curious, you're certainly welcome to check out my website at <a href='www.camiloward.com'>www.camiloward.com</a>");

                    return this.Json(new { message = "Success" });
                }
                else
                {
                    return this.Json(new { message = "You are already subscribed." });
                }
            }
            else
            {
                return this.Json(new { message = "Invalid Email Address" });
            }
        }

        [HttpGet]
        public ActionResult ValidateEmail(string code, string email)
        {
            DALBlog dalBlog = new DALBlog();

            if (dalBlog.ValidateSubscriber(email, code))
            {
                return RedirectToAction("EmailValidated", new { message = "Your email address has been validated.  You are now subscribed to the blog and will receive updates with each new post." });
            }

            return RedirectToAction("EmailValidated", new { message = "Your email address and passcode do not match." });
        }

        [HttpGet]
        public ActionResult EmailValidated(string message)
        {
            SubscriptionMessage model = new SubscriptionMessage();

            model.Message = message;
            return View(model);
        }
    }
}