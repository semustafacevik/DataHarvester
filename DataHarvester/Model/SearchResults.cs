using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataHarvester.Model.EntityFramework;

namespace DataHarvester.Model
{
    public abstract class SearchResults
    {
        public string SearchQuery { get; set; }
        public string SearchDate { get; set; }
        public string SearchIP { get; set; }

        public string ResultEmails { get; set; }
        public List<string> ResultEmailList { get; set; }
        

        public SearchResults()
        {
            ResultEmailList = new List<string>();
        }

        public virtual List<string> PropertySplit() 
        {
            List<string> emails = ResultEmails.Split('¨').ToList();
            emails.RemoveAt(0);

            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"[0-9]");

            foreach (var email in emails)
            {
                if(email.EndsWith(SearchQuery) && !rgx.IsMatch(email.Split('@')[0]))
                    ResultEmailList.Add(email);
            }
            return emails;
        }
        public abstract void SaveDB();
        public virtual tblResult SaveResultDB(int userID, DataHarvesterDBEntities db)
        {
            tblResult newResultDB = new tblResult
            {
                searchDate = Convert.ToDateTime(SearchDate),
                searchQuery = SearchQuery,
                userID = userID
            };
            db.tblResults.Add(newResultDB);

            return newResultDB;
        }
    }
}