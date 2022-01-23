using Genbox.Wikipedia.Internal;

namespace Genbox.Wikipedia.Enums;

public enum WikiSortOrder
{
    NotSet = 0,
    [StringValue("create_timestamp_asc")] CreateTimestampAsc,
    [StringValue("create_timestamp_desc")] CreateTimestampDesc,
    [StringValue("incoming_links_asc")] IncomingLinksAsc,
    [StringValue("incoming_links_desc")] IncomingLinksDesc,
    [StringValue("just_match")] JustMatch,
    [StringValue("last_edit_asc")] LastEditAsc,
    [StringValue("last_edit_desc")] LastEditDesc,
    [StringValue("none")] None,
    [StringValue("random")] Random,
    [StringValue("relevance")] Relevance,
    [StringValue("user_random")] UserRandom
}