using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.MetaDescription = "About Camilo Ward and a summary of his career and his plans for Freelance work and hobbies.";
            ViewBag.MetaKeywords = "Camilo, Ward, freelance, unity, articy";
            return View();
        }

        public ActionResult Portfolio()
        {
            ViewBag.MetaDescription = "Camilo Ward's current freelance portfolio from websites built with C#, MVC, SQL Server, javascript, jquery, and bootstrap.";
            ViewBag.MetaKeywords = "Camilo, Ward, freelance, C#, mvc, SQL, javascript, jquery, bootstrap, portfolio";
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        public ActionResult Blog()
        {         
            return View();
        }

        public ActionResult Freelance()
        {
            ViewBag.MetaDescription = "What Camilo Ward offers potential clients that want to use his freelance services.";
            ViewBag.MetaKeywords = "Camilo, Ward, freelance, services";
            return View();
        }

        public ActionResult ResumeDownload()
        {
            string path = Path.Combine(Server.MapPath("~/Content/Resume/"), "Camilo Ward - Resume 5_2026.pdf");

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            string contentType = MimeMapping.GetMimeMapping(path);

            return File(bytes, contentType, "Camilo Ward - Current Resume.pdf");
        }        
    }
}