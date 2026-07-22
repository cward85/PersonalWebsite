using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public class FreelanceController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.MetaDescription = "What Camilo Ward offers potential clients that want to use his freelance services.";
            ViewBag.MetaKeywords = "Camilo, Ward, freelance, services";
            return View();
        }
    }
}