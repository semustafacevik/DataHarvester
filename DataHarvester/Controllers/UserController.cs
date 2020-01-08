using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataHarvester.Model.EntityFramework;

namespace DataHarvester.Controllers
{
    public class UserController : Controller
    {
        DataHarvesterDBEntities db = new DataHarvesterDBEntities();

        [Route("Register")]
        [AllowAnonymous]
        public ActionResult New()
        {
            return View("UserForm", new tblUser());
        }

        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Save(tblUser user)
        {
            if (!ModelState.IsValid)
            {
                return View("UserForm", user);
            }
            if (user.ID == 0)
            {
                user.isActive = true;
                db.tblUsers.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login", "Security");
            }
            else
            {
                user.isActive = true;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                Session["user"] = user;

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Update()
        {
            var model = db.tblUsers.Find((int)Session["userID"]);
            return View("UserForm", model);
        }

        [HttpGet]
        public ActionResult Delete()
        {
            var model = db.tblUsers.Find((int)Session["userID"]);
            model.isActive = false;
            db.SaveChanges();

            return RedirectToAction("Logout", "Security");
        }

        [AllowAnonymous]
        public ActionResult IsAuthenticated()
        {
            return Content(User.Identity.IsAuthenticated.ToString());
        }
    }
}