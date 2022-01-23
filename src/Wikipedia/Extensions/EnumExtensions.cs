using System.Reflection;
using System.Text;
using Genbox.Wikipedia.Internal;

namespace Genbox.Wikipedia.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Convert a flags enum into a sequence of string values concatenated with |. E.g. Value1|Value2.
    /// </summary>
    public static string GetConcatValues(this Enum value)
    {
        StringBuilder sb = new StringBuilder();

        foreach (Enum e in Enum.GetValues(value.GetType()))
        {
            //This library follows a convention where all enums have a NotSet value. We do not want that in our output
            if (string.Equals(e.ToString(), "NotSet", StringComparison.OrdinalIgnoreCase))
                continue;

            //Same as above
            if (string.Equals(e.ToString(), "All", StringComparison.OrdinalIgnoreCase))
                continue;

            if (value.HasFlag(e))
                sb.Append(e.GetStringValue()).Append('|');
        }

        return sb.ToString().TrimEnd('|');
    }

    /// <summary>Will get the string value for a given enums value, this will only work if you assign the StringValue attribute
    /// to the items in your enum. Source:
    /// http://weblogs.asp.net/stefansedich/archive/2008/03/12/enum-with-string-values-in-c.aspx</summary>
    public static string GetStringValue(this Enum value)
    {
        // Get the type
        Type type = value.GetType();

        // Get fieldinfo for this type
        FieldInfo fieldInfo = type.GetField(value.ToString());

        // Get the stringvalue attributes
        StringValueAttribute[] attr = fieldInfo.GetCustomAttributes<StringValueAttribute>(false).ToArray();

        if (attr.Length == 0)
            throw new InvalidOperationException("Unable to find StringValue attribute on " + type.Name);

        // Return the first if there was a match.
        return attr[0].StringValue;
    }
}