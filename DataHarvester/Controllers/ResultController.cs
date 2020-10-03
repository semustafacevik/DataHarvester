using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataHarvester.Model;

namespace DataHarvester.Controllers
{
    [AllowAnonymous]
    public class ResultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Results()
        {
            ResultMember rm = new ResultMember();
            return View("Results", rm);
        }




        public ActionResult PrintResult()
        {
            return null;
        }
    }
}