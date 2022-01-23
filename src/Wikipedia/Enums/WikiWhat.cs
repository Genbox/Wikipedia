using Genbox.Wikipedia.Internal;

namespace Genbox.Wikipedia.Enums;

public enum WikiWhat
{
    NotSet = 0,

    /// <summary>Search in page titles (default) (if search engine doesn't support title searches, such as Lucene which is used
    /// by Wikipedia, then it falls back to text)</summary>
    [StringValue("title")] Title,

    /// <summary>Search in page text</summary>
    [StringValue("text")] Text,

    /// <summary>Search for titles that match exactly. Example: 'Microsoft' results in 'Microsoft', where 'Microsof' results in
    /// 'no results'</summary>
    [StringValue("nearmatch")] NearMatch
}