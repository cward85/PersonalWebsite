using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public class PortfolioController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.MetaDescription = "Camilo Ward's current freelance portfolio from websites built with C#, MVC, SQL Server, javascript, jquery, and bootstrap.";
            ViewBag.MetaKeywords = "Camilo, Ward, freelance, C#, mvc, SQL, javascript, jquery, bootstrap, portfolio";
            return View();
        }
    }
}