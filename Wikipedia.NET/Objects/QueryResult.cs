using System.Collections.Generic;
using RestSharp.Deserializers;

namespace WikipediaNET.Objects
{
    public class QueryResult
    {
        [DeserializeAs(Name = "searchinfo")]
        public SearchInfo SearchInfo { get; set; }

        [DeserializeAs(Name = "search")]
        public List<Search> Search { get; set; }

        [DeserializeAs(Name = "servedby")]
        public string ServedBy { get; set; }

        [DeserializeAs(Name = "error")]
        public Error Error { get; set; }
    }
}