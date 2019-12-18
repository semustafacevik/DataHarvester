using DataHarvester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataHarvester.Parsers
{
    public class ResultCleaner
    {
        private string DisinfectedResult { get; set; }

        public ResultCleaner(string result)
        {
            CleanResult(result);
        }

        public string GetCleanResult()
        {
            return DisinfectedResult;
        }

        private void CleanResult(string result)
        {
            result = result.Replace("<title>", " ").Replace("</title>", " ").Replace("<p>", " ").Replace("/p", " ").Replace("<cite>", " ").Replace("</cite>", " ").Replace("&quot", " ").Replace("&nbsp", " ").Replace("<span>", " ").Replace("</span>", " ");

            string[] dirtyItems = new string[] {
                "<em>",
                "</em>",
                "<b>",
                "</b>",
                "%2f",
                "%3a",
                "<strong>",
                "</strong>",
                "<wbr>",
                "</wbr>",
                "<",
                ">",
                ":",
                "=",
                ";",
                "&",
                "%3A",
                "%3D",
                "%3C",
                "/",
                "\\"
            };

            foreach (string dirtyItem in dirtyItems)
            {
                result = result.Replace(dirtyItem, "");
            }

            DisinfectedResult = result;
        }
    }
}