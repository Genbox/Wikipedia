namespace Genbox.Wikipedia.Objects;

public class ModuleError
{
    public string? Code { get; set; }

    public string? Module { get; set; }

    public string? Html { get; set; }

    public string? Text { get; set; }

    public string? Key { get; set; }

    public IList<string>? Params { get; set; }
}