using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DataHarvester.Parsers
{
    public class Regex_Mail
    {
        private string Pattern { get; set; }
        private RegexOptions Options { get; set; }
        private HashSet<string> UniqueMails { get; set; }
        private HashSet<string> UniqueAllMailFormats { get; set; }
        
        public Regex_Mail(string result, string domain)
        {
            Pattern = @"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)";
            Options = RegexOptions.Multiline;

            SearhMails(result, domain);
        }

        public IEnumerable<string> GetMails()
        {
            return UniqueMails;
        }

        public IEnumerable<string> GetAllMailFormats()
        {
            return UniqueAllMailFormats;
        }

        private void SearhMails(string result, string domain)
        {
            List<string> mails = new List<string>();
            List<string> allMailFormats = new List<string>();

            foreach (Match match in Regex.Matches(result, Pattern, Options))
            {
                allMailFormats.Add(match.Value);

                if (match.Value.EndsWith(domain) && !match.Value.Contains("http") && !match.Value.Contains("value") && !match.Value.Contains("x22"))
                    mails.Add(match.Value);
            }

            UniqueMails = new HashSet<string>(mails);
            UniqueAllMailFormats = new HashSet<string>(allMailFormats);
        }
    }
}      