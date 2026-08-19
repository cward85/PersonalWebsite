using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public class ChangelogController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.MetaDescription = "Website changelog that shows what updates have been added to the site.";
            ViewBag.MetaKeywords = "Change Log, Web Dev, updates";

            return View();
        }
    }
}