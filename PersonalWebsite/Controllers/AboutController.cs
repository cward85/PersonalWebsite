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
    public class AboutController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.MetaDescription = "About Camilo Ward and a summary of his career and his plans for Freelance work and hobbies.";
            ViewBag.MetaKeywords = "Camilo, Ward, freelance, unity, articy";
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