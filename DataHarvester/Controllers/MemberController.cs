using DataHarvester.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataHarvester.Model.EntityFramework;

namespace DataHarvester.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        DataHarvesterDBEntities db;

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Result()
        {
            db = new DataHarvesterDBEntities();

            tblResult resultDB = new tblResult();
            resultDB = db.tblResults.Where(x=>x.userID == 104).ToList().Last();

            ResultMember rm = new ResultMember();
            rm.SearchQuery = resultDB.searchQuery;
            rm.SearchDate = resultDB.searchDate.ToString();

            List<tblEmail> emails = db.tblEmails.Where(x => x.resultID == resultDB.ID).ToList();

            foreach (var item in emails)
            {
                if (item.emailAddress.Contains(rm.SearchQuery))
                    rm.ResultEmailList.Add(item.emailAddress);
                rm.ResultAllEmailList.Add(item.emailAddress);
            }


            return View("Results",rm);
        }

       
    }
}