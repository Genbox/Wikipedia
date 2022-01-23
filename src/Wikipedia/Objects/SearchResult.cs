using System.Text.Json.Serialization;

namespace Genbox.Wikipedia.Objects;

public class SearchResult
{
    //Default properties
    [JsonPropertyName("ns")]
    public int Namespace { get; set; }

    public string Title { get; set; } = string.Empty;

    public int PageId { get; set; }

    public int Size { get; set; }

    //Other properties
    public int? WordCount { get; set; }

    public DateTimeOffset? TimeStamp { get; set; }

    public string? Snippet { get; set; }

    public string? TitleSnippet { get; set; }

    public string? RedirectTitle { get; set; }

    public string? RedirectSnippet { get; set; }

    public string? SectionTitle { get; set; }

    public string? SectionSnippet { get; set; }

    public bool? IsFileMatch { get; set; }

    public string? CategorySnippet { get; set; }

    public string? ExtensionData { get; set; }

    /// <summary>The URI that points to the wikipedia page that contains the title. Note: Normalization of the title occurs
    /// automatically.</summary>
    public Uri Url { get; set; } = null!;
}