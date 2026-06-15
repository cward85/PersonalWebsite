using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Portfolio()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Resume()
        {
            ViewBag.Message = "Your contact page.";

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