using Genbox.Wikipedia.Enums;

namespace Genbox.Wikipedia;

public abstract class WikiMediaRequest
{
    /// <summary>Verify that the user is logged in if set to user, not logged in if set to anon, or has the bot user right if
    /// bot.</summary>
    public string? Assert { get; set; }

    /// <summary>Verify the current user is the named user.</summary>
    public string? AssertUser { get; set; }

    /// <summary>Include the hostname that served the request in the results.</summary>
    public bool IncludeServedBy { get; set; }

    /// <summary>Include the current timestamp in the result.</summary>
    public bool IncludeCurrentTimestamp { get; set; }

    /// <summary>Include the languages used for uselang and errorlang in the result.</summary>
    public bool IncludeLanguageUsed { get; set; }

    /// <summary>Any value given here will be included in the response. May be used to distinguish requests.</summary>
    public string? RequestId { get; set; }

    /// <summary>Language to use for message translations. action=query&meta=siteinfo with siprop=languages returns a list of
    /// language codes, or specify user to use the current user's language preference, or specify content to use this wiki's
    /// content language.</summary>
    public string? LanguageToUse { get; set; }

    /// <summary>Variant of the language. Only works if the base language supports variant conversion.</summary>
    public string? LanguageVariant { get; set; }

    /// <summary>Format to use for warning and error text output</summary>
    public WikiErrorFormat ErrorFormat { get; set; }

    /// <summary>Language to use for warnings and errors. action=query&meta=siteinfo with siprop=languages returns a list of
    /// language codes, or specify content to use this wiki's content language, or specify uselang to use the same value as the
    /// uselang parameter.</summary>
    public string? ErrorLanguageToUse { get; set; }

    /// <summary>If given, error texts will use locally-customized messages from the MediaWiki namespace.</summary>
    public bool ErrorUseLocalLanguage { get; set; }
}