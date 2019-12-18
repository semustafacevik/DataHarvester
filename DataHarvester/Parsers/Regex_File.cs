using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DataHarvester.Parsers
{
    public class Regex_File
    {
        private string Pattern { get; set; }
        private RegexOptions Options { get; set; }
        private HashSet<string> UniqueFileUrls { get; set; }

        public Regex_File(string result)
        {
            Pattern = @"<a href=\""(.*?)\""";
            Options = RegexOptions.Multiline;

            SearchFileUrls(result);
        }

        public IEnumerable<string> GetFileUrls()
        {
          return UniqueFileUrls;
        }

        private void SearchFileUrls(string result)
        {
            List<string> fileUrls = new List<string>();

            foreach (Match match in Regex.Matches(result, Pattern, Options))
            {
                if (match.Value.Contains("pdf") || match.Value.Contains("doc") || match.Value.Contains("ppt") || match.Value.Contains("xls") || match.Value.Contains("csv"))
                {
                    string url = match.Value.Replace("<a href=\"", "").Replace("\"", "");
                    fileUrls.Add(url);
                }
            }
            UniqueFileUrls = new HashSet<string>(fileUrls);
        }
    }
}