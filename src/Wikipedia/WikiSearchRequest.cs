using Genbox.Wikipedia.Enums;

namespace Genbox.Wikipedia;

public class WikiSearchRequest
{
    public WikiSearchRequest(string query)
    {
        Query = query;
        Infos = new List<Info>();
        Namespaces = new List<int>();
        Properties = new List<Property>();
    }

    public string Query { get; }

    /// <summary>What language to use. Default: English (en)</summary>
    public Language Language { get; set; }

    /// <summary>What metadata to return. Default: TotalHits, Suggestion</summary>
    public List<Info> Infos { get; set; }

    /// <summary>How many total pages to return. Default: 10 Max: 50</summary>
    public int Limit { get; set; }

    /// <summary>Use this value to continue paging (return by query). Default: 0</summary>
    public int Offset { get; set; }

    /// <summary>The namespace(s) to enumerate. When the list is empty, it implicitly contains 0, the default namespace to
    /// search.</summary>
    public List<int> Namespaces { get; set; }

    /// <summary>What property to include in the results. Defaults to a combination of snippet, size, word count and timestamp</summary>
    public List<Property> Properties { get; set; }

    /// <summary>Include redirect pages in the search.</summary>
    public bool Redirects { get; set; }

    /// <summary>Gets or sets the place to search.</summary>
    public What What { get; set; }

    /// <summary>Include the hostname that served the request in the results. Unconditionally shown on error.</summary>
    public bool IncludeServedBy { get; set; }

    /// <summary>Request ID to distinguish requests. This will just be output back to you.</summary>
    public string? RequestId { get; set; }
}