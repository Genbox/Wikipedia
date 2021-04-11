using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Genbox.Wikipedia.Misc
{
    public static class ExtensionMethods
    {
        public static bool HasElements<T>(this List<T>? list)
        {
            return list != null && list.Count >= 1;
        }

        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// Source: http://weblogs.asp.net/stefansedich/archive/2008/03/12/enum-with-string-values-in-c.aspx
        /// </summary>
        /// <param name="value"></param>
        public static string? GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attr = fieldInfo.GetCustomAttributes<StringValueAttribute>(false).ToArray();

            // Return the first if there was a match.
            return attr.Length > 0 ? attr[0].StringValue : null;
        }
    }
}
