using System;
using RestSharp.Deserializers;

namespace WikipediaNET.Objects
{
    public class Search
    {
        [DeserializeAs(Name = "ns")]
        public int NS { get; set; }

        [DeserializeAs(Name = "title")]
        public string Title { get; set; }

        [DeserializeAs(Name = "snippet")]
        public string Snippet { get; set; }

        [DeserializeAs(Name = "titlesnippet")]
        public string TitleSnippet { get; set; }

        [DeserializeAs(Name = "redirecttitle")]
        public string RedirectTitle { get; set; }

        [DeserializeAs(Name = "redirectsnippet")]
        public string RedirectSnippet { get; set; }

        [DeserializeAs(Name = "sectiontitle")]
        public string SectionTitle { get; set; }

        [DeserializeAs(Name = "sectionsnippet")]
        public string SectionSnippet { get; set; }

        [DeserializeAs(Name = "size")]
        public int Size { get; set; }

        [DeserializeAs(Name = "wordcount")]
        public int WordCount { get; set; }

        [DeserializeAs(Name = "timestamp")]
        public DateTime TimeStamp { get; set; }

        //TODO: Not tested - type not determined
        [DeserializeAs(Name = "score")]
        public string Score { get; set; }

        //TODO: Acutally not tested - type not determined
        [DeserializeAs(Name = "hasrelated")]
        public string HasRelated { get; set; }

        /// <summary>
        /// The URI that points to the wikipedia page that contains the title.
        /// Note: Normalization of the title occurs automatically.
        /// </summary>
        public Uri Url { get; set; }
    }
}