namespace WikipediaNET.Enums
{
    public enum What
    {
        /// <summary>
        /// Search in page titles (default) (if search engine doesn't support title searches, such as Lucene which is used by Wikipedia, then it falls back to text)
        /// </summary>
        Title,

        /// <summary>
        /// Search in page text
        /// </summary>
        Text,

        /// <summary>
        /// Search for titles that match excatly.
        /// Example:
        /// 'Microsoft' results in 'Microsoft'
        /// 'Microsof' results in 'no results'
        /// </summary>
        NearMatch
    }
}