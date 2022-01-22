namespace Genbox.Wikipedia.Objects;

public class WikiSearchResponse
{
    public WikiSearchResponse()
    {
        Search = new List<Search>(0);
    }

    public SearchInfo? SearchInfo { get; set; }

    public List<Search> Search { get; set; }

    public string? ServedBy { get; set; }

    public Error? Error { get; set; }
}