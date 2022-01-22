namespace Genbox.Wikipedia.Objects;

public class Search
{
    public int NS { get; set; }

    public string? Title { get; set; }

    public string? Snippet { get; set; }

    public string? TitleSnippet { get; set; }

    public string? RedirectTitle { get; set; }

    public string? RedirectSnippet { get; set; }

    public string? SectionTitle { get; set; }

    public string? SectionSnippet { get; set; }

    public int Size { get; set; }

    public int WordCount { get; set; }

    public DateTime TimeStamp { get; set; }

    //TODO: Not tested - type not determined
    public string? Score { get; set; }

    //TODO: Acutally not tested - type not determined
    public string? HasRelated { get; set; }

    /// <summary>The URI that points to the wikipedia page that contains the title. Note: Normalization of the title occurs
    /// automatically.</summary>
    public Uri Url { get; set; } = null!;
}