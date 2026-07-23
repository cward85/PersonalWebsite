using PersonalWebsite.DAL;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonalWebsite.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Login(string userName, string password)            
        {
            if (userName != null && password != null)
            {
                DALLogin dalLogin = new DALLogin();

                var model = dalLogin.Login(userName, password, Request.UserHostAddress);

                if (model.id > 0)
                {
                    HttpContext.Session["UserId"] = model.id;
                    FormsAuthentication.SetAuthCookie(model.userName, true);
                    Session["Name"] = model.name;
                    Session["IsAdmin"] = model.isAdmin;

                    return this.Json(new { message = "Success" });
                }
                else
                {
                    return this.Json(new { message = "Invalid User Name or Password." });
                }
            }

            return this.Json(new { message = "User Name and Password are required." });
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Remove("Name");            
            Session.Remove("UserId");
            Session.Remove("IsAdmin");
            Session.Clear();
            FormsAuthentication.SignOut();

            return this.Json(new { message = "Success" });
        }
    }
}