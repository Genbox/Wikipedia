using Genbox.Wikipedia.Internal;

namespace Genbox.Wikipedia.Enums;

[Flags]
public enum WikiInfo
{
    NotSet = 0,

    /// <summary>The number of search results</summary>
    [StringValue("totalhits")] TotalHits = 1 << 0,

    /// <summary>A suggestion that might fit better than what you searched for.</summary>
    [StringValue("suggestion")] Suggestion = 1 << 1,

    [StringValue("rewrittenquery")] RewrittenQuery = 1 << 2
}