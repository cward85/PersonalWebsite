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
        public ActionResult About()
        {            
            return View();
        }

        public ActionResult Portfolio()
        {
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

        public ActionResult ResumeDownload()
        {
            string path = Path.Combine(Server.MapPath("~/Content/Resume/"), "Camilo Ward - Resume 5_2026.pdf");

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            string contentType = MimeMapping.GetMimeMapping(path);

            return File(bytes, contentType, "Camilo Ward - Current Resume.pdf");
        }
    }
}