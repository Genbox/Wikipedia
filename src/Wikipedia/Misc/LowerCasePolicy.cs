using System.Text.Json;

namespace Genbox.Wikipedia.Misc;

internal class LowerCasePolicy : JsonNamingPolicy
{
    public static readonly JsonNamingPolicy Instance = new LowerCasePolicy();

    private LowerCasePolicy() { }

    public override string ConvertName(string name)
    {
        return name.ToLowerInvariant();
    }
}