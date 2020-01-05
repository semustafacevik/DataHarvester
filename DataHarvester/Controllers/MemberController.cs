using DataHarvester.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataHarvester.Model.EntityFramework;
using System.Net.Http;

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

        public ActionResult Search(string query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://127.0.0.1:5000/");

                var responseTask = client.GetAsync("/membersearch/" + query);
                try
                {
                    responseTask.Wait();
                }
                catch (Exception)
                {
                    return PartialView("_SearchError");
                }

                var result = responseTask.Result;
                ResultMember rm;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ResultMember>();
                    readTask.Wait();

                    rm = readTask.Result;
                    rm.SaveDB();

                    return PartialView("Results", rm);
                }
            }
            return PartialView("_SearchError");
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