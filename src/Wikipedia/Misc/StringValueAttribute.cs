namespace Genbox.Wikipedia.Misc;

/// <summary>This attribute is used to represent a string value for a value in an enum.</summary>
[AttributeUsage(AttributeTargets.Field)]
internal class StringValueAttribute : Attribute
{
    /// <summary>Constructor used to init a StringValue Attribute</summary>
    public StringValueAttribute(string value)
    {
        StringValue = value;
    }

    /// <summary>Holds the string value for a value in an enum.</summary>
    public string StringValue { get; }
}