using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataHarvester.Model.EntityFramework;

namespace DataHarvester.Model
{
    public class ResultMember : SearchResults
    {
        public List<string> ResultAllEmailList { get; set; }
        
        public string ResultFileUrls { get; set; }
        public List<string> ResultFileUrlList { get; set; }

        public string ResultHostnames { get; set; }
        public List<string> ResultHostnameList { get; set; }

        public string ResultIPs { get; set; }
        public List<string> ResultIPList { get; set; }

        public string ResultLinkedInLinks { get; set; }
        public List<string> ResultLinkedInLinkList { get; set; }

        public string ResultLinkedInProfiles { get; set; }
        public List<string> ResultLinkedInProfileList { get; set; }

        public string ResultPorts { get; set; }
        public List<string> ResultPortList { get; set; }

        public ResultMember()
        {
            ResultAllEmailList = new List<string>();
            ResultFileUrlList = new List<string>();
            ResultHostnameList = new List<string>();
            ResultIPList = new List<string>();
            ResultLinkedInLinkList = new List<string>();
            ResultLinkedInProfileList = new List<string>();
            ResultPortList = new List<string>();
        }

        public override List<string> PropertySplit()
        {
            ResultAllEmailList = base.PropertySplit();

            foreach (var property in typeof(ResultMember).GetProperties())
            {
                if (property.PropertyType == typeof(string))
                {
                    string propertyValue = (string)property.GetValue(this);
                    List<string> propertyList = propertyValue.Split('¨').ToList();
                    propertyList.RemoveAt(0);

                    switch (property.Name)
                    {
                        case "ResultFileUrls":
                            ResultFileUrlList = propertyList;
                            break;

                        case "ResultHostnames":
                            ResultHostnameList = propertyList;
                            break;

                        case "ResultIPs":
                            ResultIPList = propertyList;
                            break;

                        case "ResultLinkedInLinks":
                            ResultLinkedInLinkList = propertyList;
                            break;

                        case "ResultLinkedInProfiles":
                            ResultLinkedInProfileList = propertyList;
                            break;

                        case "ResultPorts":
                            ResultPortList = propertyList;
                            break;

                        default:
                            break;
                    }
                }
            }
            return ResultAllEmailList;
        }

        DataHarvesterDBEntities db;
        public override void SaveDB()
        {
            db = new DataHarvesterDBEntities();
            tblResult newResult = base.SaveResultDB(103, db);

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