using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using DataHarvester.Model.EntityFramework;

namespace DataHarvester.Model
{
    public class SearchResults
    {
        public string SearchQuery { get; set; }
        public string SearchDate { get; set; }

        public string ResultEmails { get; set; }
        private List<string> ResultAllEmailList { get; set; }

        public string ResultFileUrls { get; set; }
        private List<string> ResultFileUrlList { get; set; }

        public string ResultHostnames { get; set; }
        private List<string> ResultHostnameList { get; set; }

        public string ResultIPs { get; set; }
        private List<string> ResultIPList { get; set; }

        public string ResultLinkedInLinks { get; set; }
        private List<string> ResultLinkedInLinkList { get; set; }

        public string ResultLinkedInProfiles { get; set; }
        private List<string> ResultLinkedInProfileList { get; set; }

        public string ResultPorts { get; set; }
        private List<string> ResultPortList { get; set; }

        DataHarvesterDBEntities db;

        private void PropertySplit()
        {
            foreach (var property in typeof(SearchResults).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                string propertyValue = (string)property.GetValue(this);
                List<string> propertyList = propertyValue.Split('¨').ToList();
                propertyList.RemoveAt(0);

                switch (property.Name)
                {
                    case "ResultEmails":
                        ResultAllEmailList = new List<string>();
                        ResultAllEmailList = propertyList;
                        break;

                    case "ResultFileUrls":
                        ResultFileUrlList = new List<string>();
                        ResultFileUrlList = propertyList;
                        break;

                    case "ResultHostnames":
                        ResultHostnameList = new List<string>();
                        ResultHostnameList = propertyList;
                        break;

                    case "ResultIPs":
                        ResultIPList = new List<string>();
                        ResultIPList = propertyList;
                        break;

                    case "ResultLinkedInLinks":
                        ResultLinkedInLinkList = new List<string>();
                        ResultLinkedInLinkList = propertyList;
                        break;

                    case "ResultLinkedInProfiles":
                        ResultLinkedInProfileList = new List<string>();
                        ResultLinkedInProfileList = propertyList;
                        break;

                    case "ResultPorts":
                        ResultPortList = new List<string>();
                        ResultPortList = propertyList;
                        break;

                    default:
                        break;
                }
            }
        }

        public void SaveAllDB(int userID)
        {
            db = new DataHarvesterDBEntities();
            tblResult newResult = SaveResultDB(userID);

            PropertySplit();

            SaveEmailDB(newResult.ID);
            SaveFileUrlDB(newResult.ID);
            SaveHostnameDB(newResult.ID);
            SaveIPDB(newResult.ID);
            SaveLinkedInLinkDB(newResult.ID);
            SaveLinkedInProfileDB(newResult.ID);
            SavePortDB(newResult.ID);

            db.SaveChanges();
        }

        private tblResult SaveResultDB(int userID)
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
        private void SaveEmailDB(int resultID)
        {
            foreach (var email in ResultAllEmailList)
            {
                tblEmail newEmailDB = new tblEmail()
                {
                    emailAddress = email,
                    resultID = resultID
                };
                db.tblEmails.Add(newEmailDB);
            }
        }
        private void SaveFileUrlDB(int resultID)
        {
            foreach (var fileUrl in ResultFileUrlList)
            {
                tblFileUrl newFileUrlDB = new tblFileUrl()
                {
                    fileUrlLink = fileUrl,
                    resultID = resultID
                };
                db.tblFileUrls.Add(newFileUrlDB);
            }
        }
        private void SaveHostnameDB(int resultID)
        {
            foreach (var hostname in ResultHostnameList)
            {
                tblHost newHostDB = new tblHost()
                {
                    hostname = hostname,
                    resultID = resultID
                };
                db.tblHosts.Add(newHostDB);
            }
        }
        private void SaveIPDB(int resultID)
        {
            foreach (var IP in ResultIPList)
            {
                tblIP newIPDB = new tblIP()
                {
                    IPAddress = IP,
                    resultID = resultID
                };
                db.tblIPs.Add(newIPDB);
            }
        }
        private void SaveLinkedInLinkDB(int resultID)
        {
            foreach (var linkedInLink in ResultLinkedInLinkList)
            {
                tblLinkedInLink newLinkedInLinkDB = new tblLinkedInLink()
                {
                    link = linkedInLink,
                    resultID = resultID
                };
                db.tblLinkedInLinks.Add(newLinkedInLinkDB);
            }
        }
        private void SaveLinkedInProfileDB(int resultID)
        {
            foreach (var linkedInProfile in ResultLinkedInProfileList)
            {
                tblLinkedInProfile newLinkedInProfileDB = new tblLinkedInProfile()
                {
                    profile = linkedInProfile,
                    resultID = resultID
                };
                db.tblLinkedInProfiles.Add(newLinkedInProfileDB);
            }
        }
        private void SavePortDB(int resultID)
        {
            foreach (var port in ResultPortList)
            {
                tblPort newPortDB = new tblPort()
                {
                    openPort = port,
                    resultID = resultID
                };
                db.tblPorts.Add(newPortDB);
            }
        }
    }
}