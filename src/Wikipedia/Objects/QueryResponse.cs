namespace Genbox.Wikipedia.Objects;

public class QueryResponse
{
    public string? BatchComplete { get; set; }
    public Continuation? Continue { get; set; }
    public WikiSearchResponse? Query { get; set; }
}