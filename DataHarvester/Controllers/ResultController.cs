using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataHarvester.Model;

namespace DataHarvester.Controllers
{
    public class ResultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Results(SearchResults sr)
        {
            sr = new SearchResults();
            return View(sr);
        }
    }
}