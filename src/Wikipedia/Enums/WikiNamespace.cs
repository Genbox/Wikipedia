using Genbox.Wikipedia.Internal;

namespace Genbox.Wikipedia.Enums;

/// <summary>The Wikipedia namespaces. See https://en.wikipedia.org/wiki/Wikipedia:Namespace</summary>
[Flags]
public enum WikiNamespace : ulong
{
    NotSet = 0,
    [StringValue("*")] All = 1 << 0,
    [StringValue("0")] Main = 1 << 1,
    [StringValue("1")] Talk = 1 << 2,
    [StringValue("2")] User = 1 << 3,
    [StringValue("3")] UserTalk = 1 << 4,
    [StringValue("4")] Wikipedia = 1 << 5,
    [StringValue("5")] WikipediaTalk = 1 << 6,
    [StringValue("6")] File = 1 << 7,
    [StringValue("7")] FileTalk = 1 << 8,
    [StringValue("8")] MediaWiki = 1 << 9,
    [StringValue("9")] MediaWikiTalk = 1 << 10,
    [StringValue("10")] Template = 1 << 11,
    [StringValue("11")] TemplateTalk = 1 << 12,
    [StringValue("12")] Help = 1 << 13,
    [StringValue("13")] HelpTalk = 1 << 14,
    [StringValue("14")] Category = 1 << 15,
    [StringValue("15")] CategoryTalk = 1 << 16,
    [StringValue("100")] Portal = 1 << 17,
    [StringValue("101")] PortalTalk = 1 << 18,
    [StringValue("118")] Draft = 1 << 19,
    [StringValue("119")] DraftTalk = 1 << 20,
    [StringValue("710")] TimedText = 1 << 21,
    [StringValue("711")] TimedTextTalk = 1 << 22,
    [StringValue("828")] Module = 1 << 23,
    [StringValue("829")] ModuleTalk = 1 << 24
}