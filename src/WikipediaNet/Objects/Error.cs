using RestSharp.Deserializers;

namespace WikipediaNet.Objects
{
    public class Error
    {
        [DeserializeAs(Name = "code")]
        public string Code { get; set; }

        [DeserializeAs(Name = "info")]
        public string Info { get; set; }
    }
}