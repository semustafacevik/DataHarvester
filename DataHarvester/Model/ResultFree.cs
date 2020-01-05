using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataHarvester.Model.EntityFramework;

namespace DataHarvester.Model
{
    public class ResultFree : SearchResults
    {
        public override List<string> PropertySplit()
        {
            base.PropertySplit();
            try
            {
                base.ResultEmailList.RemoveRange(5, ResultEmailList.Count() - 5);
            }
            catch { }
            return ResultEmailList;
        }

        DataHarvesterDBEntities db;
        public override void SaveDB()
        {
            db = new DataHarvesterDBEntities();
            tblResult newResult = base.SaveResultDB(CheckUserIP(), db);

            PropertySplit();

            SaveEmailDB_Free(newResult.ID);
            db.SaveChanges();
        }
    
        private int CheckUserIP()
        {
            tblUser user = db.tblUsers.Where(x => x.username == SearchIP).FirstOrDefault();
            if(user == null)
            {
                user = new tblUser() {
                    username = SearchIP,
                    password = "pS" + DateTime.Now.ToString(),
                    name = "Free Account",
                    emailAddress = "free@account.com"
                };
                db.tblUsers.Add(user);
            }
            return user.ID;
        }

        private void SaveEmailDB_Free(int resultID)
        {
            foreach (var email in base.ResultEmailList)
            {
                tblEmail newEmailDB = new tblEmail()
                {
                    emailAddress = email,
                    resultID = resultID
                };
                db.tblEmails.Add(newEmailDB);
            }
        }
    }
}