using Genbox.Wikipedia.Enums;

namespace Genbox.Wikipedia;

public class WikiSearchRequest : WikiMediaRequest
{
    //See https://www.mediawiki.org/wiki/API:Search

    public WikiSearchRequest(string query)
    {
        Query = query;
    }

    /// <summary>Search for page titles or content matching this value. You can use the search string to invoke special search
    /// features, depending on what the wiki's search backend implements.</summary>
    public string Query { get; set; }

    /// <summary>The namespace(s) to enumerate. Defaults to <see cref="WikiNamespace.Main" />.</summary>
    public WikiNamespace NamespacesToInclude { get; set; }

    /// <summary>How many total pages to return. Default: 10, Max: 500</summary>
    public int Limit { get; set; }

    /// <summary>When more results are available, use this to continue. Default: 0</summary>
    public int Offset { get; set; }

    /// <summary>Query independent profile to use (affects ranking algorithm). Defaults to
    /// <see cref="WikiQueryProfile.AutoSelect" /></summary>
    public WikiQueryProfile QueryIndependentProfile { get; set; }

    /// <summary>Which type of search to perform.</summary>
    public WikiWhat WhatToSearch { get; set; }

    /// <summary>What metadata to return. Default: TotalHits, Suggestion</summary>
    public WikiInfo InfoToInclude { get; set; }

    /// <summary>What property to include in the results. Defaults to a combination of snippet, size, word count and timestamp</summary>
    public WikiProperty PropertiesToInclude { get; set; }

    /// <summary>Include InterWiki results in the search, if available.</summary>
    public bool IncludeInterWikiResults { get; set; }

    /// <summary>Enable internal query rewriting. Some search backends can rewrite the query into another which is thought to
    /// provide better results, for instance by correcting spelling errors.</summary>
    public bool EnableRewrites { get; set; }

    /// <summary>Set the sort order of returned results. Defaults to Relevance.</summary>
    public WikiSortOrder SortOrder { get; set; }

    /// <summary>What language to use. Default: English (en)</summary>
    public WikiLanguage WikiLanguage { get; set; }

    public bool TryValidate(out string? message)
    {
        if (Limit > 500)
        {
            message = nameof(Limit) + " must be between 1 and 500";
            return false;
        }

        if (string.IsNullOrEmpty(Query))
        {
            message = nameof(Query) + " must be set to a value";
            return false;
        }

        if (WikiLanguage == WikiLanguage.NotSet)
        {
            message = nameof(WikiLanguage) + " must be set to a valid value";
            return false;
        }

        message = null;
        return true;
    }
}