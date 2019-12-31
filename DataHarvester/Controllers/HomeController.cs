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
        SearchResults searchResults = null;
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
                    return RedirectToAction("Index");
                }

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<SearchResults>();
                    readTask.Wait();

                    searchResults = readTask.Result;
                    searchResults.SaveAllDB(100);

                    return View("Results", searchResults);
                }
            }

            return RedirectToAction("Index");
        }
    }
}