using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Genbox.Wikipedia.Enums;
using Genbox.Wikipedia.Misc;
using Genbox.Wikipedia.Objects;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serialization.Xml;

namespace Genbox.Wikipedia
{
    public class WikipediaClient
    {
        private readonly RestClient _client = new RestClient();
        private readonly Lazy<JsonDeserializer> _jsonSerializer = new Lazy<JsonDeserializer>();
        private readonly Lazy<XmlAttributeDeserializer> _xmlSerializer = new Lazy<XmlAttributeDeserializer>();
        private Format _format;
        private const string _dateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'";

        public WikipediaClient(Language language = Language.English)
        {
            Language = language;
            Format = Format.XML;

            Infos = new List<Info>();
            Namespaces = new List<int>();
            Properties = new List<Property>();
        }

        /// <summary>
        /// Set to true to use HTTPS instead of HTTP.
        /// </summary>
        public bool UseTls { get; set; }

        /// <summary>
        /// What language to use.
        /// Default: English (en)
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// Gets or sets the format to use.
        /// Note: This currently defaults only to XML - once RestSharp gets DeserializeAs attributes for JSON, I will implement support for JSON as well.
        /// </summary>
        public Format Format
        {
            get => _format;
            private set => _format = value == Format.Default ? Format.XML : value;
        }

        /// <summary>
        /// What metadata to return.
        /// Default: TotalHits, Suggestion
        /// </summary>
        public List<Info> Infos { get; set; }

        /// <summary>
        /// How many total pages to return.
        /// Default: 10
        /// Max: 50
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Use this value to continue paging (return by query).
        /// Default: 0
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// The namespace(s) to enumerate.
        /// When the list is empty, it implicitly contains 0, the default namespace to search.
        /// </summary>
        public List<int> Namespaces { get; set; }

        /// <summary>
        /// What propery to include in the results.
        /// Defaults to a combination of snippet, size, word count and timestamp
        /// </summary>
        public List<Property> Properties { get; set; }

        /// <summary>
        /// Include redirect pages in the search.
        /// </summary>
        public bool Redirects { get; set; }

        /// <summary>
        /// Gets or sets the place to search.
        /// </summary>
        public What What { get; set; }

        /// <summary>
        /// Include the hostname that served the request in the results. Unconditionally shown on error.
        /// </summary>
        public bool ServedBy { get; set; }

        /// <summary>
        /// Request ID to distinguish requests. This will just be output back to you.
        /// </summary>
        public string? RequestId { get; set; }

        private RestRequest CreateRequest(string query)
        {
            //API example: http://en.wikipedia.org/w/api.php?action=query&list=search&srsearch=wikipedia&srprop=timestamp
            _client.BaseUrl = new Uri(string.Format(UseTls ? "https://{0}.wikipedia.org/w/" : "http://{0}.wikipedia.org/w/", Language.GetStringValue()));

            RestRequest request = new RestRequest("api.php", Method.GET);

            //Required
            request.AddParameter("action", "query");
            request.AddParameter("list", "search");
            request.AddParameter("srsearch", query);
            request.AddParameter("format", Format.ToString().ToLower());

            //Optional
            if (Infos.HasElements())
                request.AddParameter("srinfo", string.Join("|", Infos).ToLower());

            if (Limit != 0)
                request.AddParameter("srlimit", Limit);

            if (Offset != 0)
                request.AddParameter("sroffset", Offset);

            if (Namespaces.HasElements())
                request.AddParameter("srnamespace", string.Join("|", Namespaces).ToLower());

            if (Properties.HasElements())
                request.AddParameter("srprop", string.Join("|", Properties).ToLower());

            if (Redirects)
                request.AddParameter("srredirects", Redirects.ToString().ToLower());

            if (What != What.Title)
                request.AddParameter("srwhat", What.ToString().ToLower());

            if (ServedBy)
                request.AddParameter("servedby", ServedBy.ToString().ToLower());

            if (!string.IsNullOrEmpty(RequestId))
                request.AddParameter("requestid", RequestId);

            return request;
        }

        public QueryResult Search(string query)
        {
            RestRequest request = CreateRequest(query);
            IRestResponse response = _client.Execute(request);
            return Deserialize(response);

        }

        public async Task<QueryResult> SearchAsync(string query, CancellationToken token = default)
        {
            RestRequest request = CreateRequest(query);
            IRestResponse response = await _client.ExecuteAsync(request, token).ConfigureAwait(false);
            return Deserialize(response);
        }

        private QueryResult Deserialize(IRestResponse response)
        {
            IDeserializer deserializer;

            switch (Format)
            {
                case Format.JSON:
                    JsonDeserializer jsonDeserializer = _jsonSerializer.Value;

                    jsonDeserializer.DateFormat = _dateFormat;
                    jsonDeserializer.RootElement = "query";

                    deserializer = jsonDeserializer;
                    break;
                default:
                    XmlAttributeDeserializer xmlDeserializer = _xmlSerializer.Value;
                    xmlDeserializer.DateFormat = _dateFormat;
                    xmlDeserializer.RootElement = "query";

                    deserializer = xmlDeserializer;
                    break;
            }

            QueryResult results = deserializer.Deserialize<QueryResult>(response);

            //For convenience, we autocreate uris that point directly to the wiki page.
            if (results.Search != null)
            {
                foreach (Search search in results.Search)
                {
                    search.Url = UseTls ? new Uri("https://" + Language.GetStringValue() + ".wikipedia.org/wiki/" + search.Title) : new Uri("http://" + Language.GetStringValue() + ".wikipedia.org/wiki/" + search.Title);
                }
            }

            return results;
        }
    }
}