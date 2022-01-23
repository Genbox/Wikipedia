using System.Text.Json.Serialization;
using Genbox.Wikipedia.Objects;

namespace Genbox.Wikipedia;

public class WikiSearchResponse
{
    public bool BatchComplete { get; set; }
    public Continuation? Continue { get; set; }
    
    [JsonPropertyName("query")]
    public QueryResult? QueryResult { get; set; }
    public Error? Error { get; set; }
    
    [JsonPropertyName("errors")]
    public IList<ModuleError>? ModuleErrors { get; set; }
    public string? ServedBy { get; set; }
    public string? RequestId { get; set; }

    [JsonPropertyName("errorlang")]
    public string? ErrorLanguage { get; set; }

    [JsonPropertyName("uselang")]
    public string? Language { get; set; }

    [JsonPropertyName("curtimestamp")]
    public DateTimeOffset CurrentTimestamp { get; set; }
}