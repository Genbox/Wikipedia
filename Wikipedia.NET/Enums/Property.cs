namespace WikipediaNET.Enums
{
    public enum Property
    {
        /// <summary>
        /// Adds the size of the page in bytes
        /// </summary>
        Size,
        /// <summary>
        /// Adds the word count of the page
        /// </summary>
        Wordcount,
        /// <summary>
        ///  Adds the timestamp of when the page was last edited
        /// </summary>
        Timestamp,
        /// <summary>
        /// Adds the score (if any) from the search engine
        /// </summary>
        Score,
        /// <summary>
        /// Adds a parsed snippet of the page
        /// </summary>
        Snippet,
        /// <summary>
        /// Adds a parsed snippet of the page title
        /// </summary>
        TitleSnippet,
        /// <summary>
        /// Adds a parsed snippet of the redirect
        /// </summary>
        RedirectSnippet,
        /// <summary>
        ///  Adds a parsed snippet of the redirect title
        /// </summary>
        RedirectTitle,
        /// <summary>
        /// Adds a parsed snippet of the matching section
        /// </summary>
        SectionSnippet,
        /// <summary>
        /// Adds a parsed snippet of the matching section title
        /// </summary>
        SectionTitle,
        /// <summary>
        /// Indicates whether a related search is available
        /// </summary>
        HasRelated
    }
}