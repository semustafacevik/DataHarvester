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

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(tblUser user)
        {
            db = new DataHarvesterDBEntities();

            var userDB = db.tblUsers.FirstOrDefault(x => x.username == user.username && x.password == user.password);

            if (userDB != null)
            {
                FormsAuthentication.SetAuthCookie(userDB.username, false);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ViewBag.Message = "Kullanıcı adı veya parola hatalı. Bilgilerinizi kontrol ediniz.";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}