using DataHarvester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DataHarvester.Parsers
{
    public class Regex_Hostname
    {
        private string Pattern { get; set; }
        private RegexOptions Options { get; set; }
        private HashSet<string> UniqueHostnames { get; set; }

        public Regex_Hostname(string result, string domain)
        {
            Pattern = @"www[\w.-]*\." + domain;
            Options = RegexOptions.Multiline;

            SearchHostnames(result);
        }

        public IEnumerable<string> GetHostnames()
        {
            return UniqueHostnames;
        }

        private void SearchHostnames(string result)
        {
            List<string> hostnames = new List<string>();

            foreach (Match match in Regex.Matches(result, Pattern, Options))
            {
                hostnames.Add(match.Value);
            }
            UniqueHostnames = new HashSet<string>(hostnames);
        }
    }
}