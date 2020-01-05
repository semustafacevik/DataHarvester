using DataHarvester.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DataHarvester.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://127.0.0.1:5000/");

                var responseTask = client.GetAsync("/search/" + query);
                try
                {
                    responseTask.Wait();
                }
                catch (Exception)
                {
                    return PartialView("_SearchError");
                }

                var result = responseTask.Result;
                ResultFree rf;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ResultFree>();
                    readTask.Wait();

                    rf = readTask.Result;
                    rf.SaveDB();

                    return PartialView("_ResultsFree", rf);
                }
            }
            return PartialView("_SearchError");
        }
    }
}