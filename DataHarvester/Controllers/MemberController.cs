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
        ResultMember rm;
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

        public ActionResult GetMailsForSelectedProfile(string name, string query)
        {
            rm = new ResultMember(query);
            List<string> mails = rm.GenerateMail(name);
            ViewBag.name = name;

            return PartialView("SelectedProfileMails", mails);
        }
    }
}