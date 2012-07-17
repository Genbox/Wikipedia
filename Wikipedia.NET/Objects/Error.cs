using RestSharp.Deserializers;

namespace WikipediaNET.Objects
{
    public class Error
    {
        [DeserializeAs(Name = "code")]
        public string Code { get; set; }

        [DeserializeAs(Name = "info")]
        public string Info { get; set; }
    }
}