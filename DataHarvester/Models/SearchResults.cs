using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataHarvester.Models
{
    public class SearchResults
    {
        public string DomainName { get; set; }
        public string Result_Bing { get; set; }
        public string Result_Google { get; set; }
        public string Result_Hunter { get; set; }
        public string Result_LinkedIn { get; set; }
        public string Result_Yahoo { get; set; }


        public string TotalResult()
        { ////////////////****************
            return Result_Bing + Result_Google + Result_Hunter + Result_LinkedIn + Result_Yahoo;
        }
    }
}