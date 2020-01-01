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
        ResultMember Result = null;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string word)
        {
            string query = "/membersearch/" + word;

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
                    return RedirectToAction("Index");
                }

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ResultMember>();
                    readTask.Wait();

                    Result = readTask.Result;
                    Result.SaveDB();

                    return View("Results", Result);
                }
            }

            return RedirectToAction("Index");
        }
    }
}