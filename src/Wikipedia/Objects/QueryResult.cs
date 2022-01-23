using System.Text.Json.Serialization;

namespace Genbox.Wikipedia.Objects;

public class QueryResult
{
    public QueryResult()
    {
        SearchResults = new List<SearchResult>(0);
    }

    [JsonPropertyName("search")]
    public List<SearchResult> SearchResults { get; set; }
    public SearchInfo? SearchInfo { get; set; }
}