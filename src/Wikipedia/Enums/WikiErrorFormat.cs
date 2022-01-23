using Genbox.Wikipedia.Internal;

namespace Genbox.Wikipedia.Enums;

public enum WikiErrorFormat
{
    NotSet = 0,

    /// <summary>Format used prior to MediaWiki 1.29. <see cref="WikiMediaRequest.ErrorLanguageToUse" /> and
    /// <see cref="WikiMediaRequest.ErrorUseLocalLanguage" /> are ignored.</summary>
    [StringValue("bc")] Bc,

    /// <summary>HTML</summary>
    [StringValue("html")] Html,

    /// <summary>No text output, only the error codes.</summary>
    [StringValue("none")] None,

    /// <summary>Wikitext with HTML tags removed and entities replaced.</summary>
    [StringValue("plaintext")] Plaintext,

    /// <summary>Message key and parameters.</summary>
    [StringValue("raw")] Raw,

    /// <summary>Unparsed wikitext.</summary>
    [StringValue("wikitext")] WikiText
}