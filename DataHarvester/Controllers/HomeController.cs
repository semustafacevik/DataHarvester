using DataHarvester.Models;
using DataHarvester.Parsers;
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
                    searchResults.DomainName = word;
                }
            }

            string totalResult = searchResults.TotalResult();

            ResultCleaner cleaner = new ResultCleaner(totalResult);
            string cleanResult = cleaner.GetCleanResult();

            Regex_Hostname r_hostname = new Regex_Hostname(cleanResult, searchResults.DomainName);
            IEnumerable<string> hostnames = r_hostname.GetHostnames();

            Regex_Mail r_mail = new Regex_Mail(cleanResult, searchResults.DomainName);
            IEnumerable<string> mails = r_mail.GetMails();
            IEnumerable<string> allMailFormats = r_mail.GetAllMailFormats();

            Regex_File r_file = new Regex_File(searchResults.TotalResult());
            IEnumerable<string> fileUrls = r_file.GetFileUrls();

            Regex_LinkedIn r_linkedin = new Regex_LinkedIn(searchResults.TotalResult());
            IEnumerable<string> linkedInProfiles = r_linkedin.GetProfiles();
            IEnumerable<string> linkedInLinks = r_linkedin.GetLinks();

            return RedirectToAction("Index");
        }
    }
}