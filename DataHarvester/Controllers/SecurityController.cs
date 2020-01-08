using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataHarvester.Model.EntityFramework;

namespace DataHarvester.Controllers
{
    public class SecurityController : Controller
    {
        DataHarvesterDBEntities db;

        [Route("Login")]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public ActionResult Login(tblUser user)
        {
            db = new DataHarvesterDBEntities();

            var userDB = db.tblUsers.FirstOrDefault(x => x.username == user.username && x.password == user.password && x.isActive == true);

            if (userDB != null)
            {
                FormsAuthentication.SetAuthCookie(userDB.ID.ToString(), false);
                Session.Add("user", userDB);
                Session.Add("userID", userDB.ID);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ViewBag.Message = "Kullanıcı adı veya şifre hatalı. Bilgilerinizi kontrol ediniz.";
                return View();
            }
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("user");
            Session.Remove("userID");
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}