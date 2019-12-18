using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DataHarvester.Parsers
{
    public class Regex_LinkedIn
    {
        private string PatternProfile { get; set; }
        private string PatternLink { get; set; }
        private RegexOptions Options { get; set; }
        private HashSet<string> UniqueProfiles { get; set; }
        private HashSet<string> UniqueLinks { get; set; }

        public Regex_LinkedIn(string result)
        {
            PatternProfile = @"[\w.,_ |\\/&-]* \| LinkedIn";
            PatternLink = @"url=https:\/\/www\.linkedin.com(.*?)&";
            Options = RegexOptions.Multiline;

            SearchProfiles(result);
            SearchLinks(result);
        }

        public IEnumerable<string> GetProfiles()
        {
            return UniqueProfiles;
        }

        public IEnumerable<string> GetLinks()
        {
            return UniqueLinks;
        }

        private void SearchProfiles(string result)
        {
            result = result.Replace("&amp;", "&");
            List<string> linkedInProfiles = new List<string>();

            foreach (Match match in Regex.Matches(result, PatternProfile, Options))
            {
                linkedInProfiles.Add(match.Value);
            }
            UniqueProfiles = new HashSet<string>(linkedInProfiles);
        }

        private void SearchLinks(string result)
        {
            result = result.Replace("tr.linkedin.com", "www.linkedin.com");
            List<string> linkedInLinks = new List<string>();

            foreach (Match match in Regex.Matches(result, PatternLink, Options))
            {
                string link = match.Value.Replace("url=", "").Replace("&", "");
                linkedInLinks.Add(link);
            }
            UniqueLinks = new HashSet<string>(linkedInLinks);
        }
    }
}