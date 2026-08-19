using PersonalWebsite.DAL;
using System;
using System.IO;
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
            ViewBag.MetaKeywords = "Camilo, Ward, freelance, unity, articy, blog, development, hobbies, career, resume, skills, microsoft, sql, c#, javascript, jquery, visual studios";
            return View();
        }

        public ActionResult ResumeDownload()
        {
            resumeDownload resume = new resumeDownload();

            resume.ipAddress = Request.UserHostAddress;
            resume.downloadDate = DateTime.Now;
            DALAbout dalAbout = new DALAbout();

            dalAbout.LogResumeDownload(resume);

            string path = Path.Combine(Server.MapPath("~/Content/Resume/"), "Camilo Ward - Current Resume.pdf");

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            string contentType = MimeMapping.GetMimeMapping(path);

            return File(bytes, contentType, "Camilo Ward - Current Resume.pdf");
        }        
    }
}