using DataHarvester.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DataHarvester.Controllers
{
    public class HomeController : Controller
    {
        //ResultFree ResultFree = null;
        ResultFree Result = null;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string word)
        {
            string query = "/search/" + word;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://127.0.0.1:5000/");

                var responseTask = client.GetAsync(query);
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
                    var readTask = result.Content.ReadAsAsync<ResultFree>();
                    readTask.Wait();

                    Result = readTask.Result;
                    Result.SaveDB();

                    return PartialView("_ResultsFree", Result);
                }
            }
            return PartialView("_SearchError");
        }

        public ActionResult Test()
        {
            ResultMember rm = new ResultMember();
            return View("Results", rm);
        }
    }
}