using Genbox.Wikipedia.Internal;

namespace Genbox.Wikipedia.Enums;

[Flags]
public enum WikiQueryProfile
{
    NotSet = 0,

    /// <summary>Ranking based on the number of incoming links, some templates, page language and recency
    /// (templates/language/recency may not be activated on this wiki).</summary>
    [StringValue("classic")] Classic = 1 << 0,

    /// <summary>Ranking based on some templates, page language and recency when activated on this wiki.</summary>
    [StringValue("classic_noboostlinks")] ClassicNoBoostLinks = 1 << 1,

    /// <summary>Ranking based solely on query dependent features (for debug only).</summary>
    [StringValue("empty")] Empty = 1 << 2,

    /// <summary>Weighted sum based on incoming links</summary>
    [StringValue("wsum_inclinks")] WeightedSumIncomingLinks = 1 << 3,

    /// <summary>Weighted sum based on incoming links and weekly pageviews</summary>
    [StringValue("wsum_inclinks_pv")] WeightedSumIncomingLinksAndPageViews = 1 << 4,

    /// <summary>Ranking based primarily on page views</summary>
    [StringValue("popular_inclinks_pv")] PopularIncomingLinksAndPageViews = 1 << 5,

    /// <summary>Ranking based primarily on incoming link counts</summary>
    [StringValue("popular_inclinks")] PopularIncomingLinks = 1 << 6,

    /// <summary>Let the search engine decide on the best profile to use.</summary>
    [StringValue("engine_autoselect")] AutoSelect = 1 << 7
}